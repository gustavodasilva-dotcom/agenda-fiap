using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Common.Shared.Abstractions;

public abstract class BaseDbContext<TDbContext>(
    DbContextOptions<TDbContext> options,
    IPublisher publisher) : DbContext(options)
    where TDbContext : DbContext
{
    private readonly IPublisher _publisher = publisher;

    IEnumerable<IDomainEvent> GetDomainEventsFromChangeTracker(
        Func<BaseEntity, IEnumerable<IDomainEvent>> selector)
        => ChangeTracker
            .Entries<BaseEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(selector);

    private void GetDomainEvents(ref Stack<IDomainEvent> eventsStack)
    {
        var domainEvents = GetDomainEventsFromChangeTracker(entity =>
        {
            var domainEvents = entity.GetDomainEvents();

            entity.ClearDomainEvents();

            return domainEvents;
        });

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            eventsStack.Push(domainEvent);
        }
    }

    private void GetEventFactories(ref Stack<IDomainEvent> eventsStack)
    {
        var eventFactories = GetDomainEventsFromChangeTracker(entity =>
        {
            var eventFactories = entity.GetEventFactories();

            entity.ClearEventFactories();

            return eventFactories;
        });

        foreach (IDomainEvent domainEvent in eventFactories)
        {
            eventsStack.Push(domainEvent);
        }
    }

    private async Task PublishDomainEventsAsync(Stack<IDomainEvent> eventsStack)
    {
        foreach (IDomainEvent domainEvent in eventsStack)
        {
            await _publisher.Publish(domainEvent);
        }

        eventsStack.Clear();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Stack<IDomainEvent> eventsStack = new();

        GetDomainEvents(ref eventsStack);

        var result = await base.SaveChangesAsync(cancellationToken);

        GetEventFactories(ref eventsStack);

        await PublishDomainEventsAsync(eventsStack);

        return result;
    }
}
