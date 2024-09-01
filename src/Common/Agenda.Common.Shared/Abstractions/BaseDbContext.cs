using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Common.Shared.Abstractions;

public abstract class BaseDbContext<TDbContext>(
    DbContextOptions<TDbContext> options,
    IPublisher publisher) : DbContext(options)
    where TDbContext : DbContext
{
    private readonly IPublisher _publisher = publisher;

    private List<IDomainEvent> GetDomainEvents()
    {
        var domainEvents = ChangeTracker
            .Entries<BaseEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        return domainEvents;
    }

    private async Task PublishDomainEventsAsync(
        List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var domainEvents = GetDomainEvents();

        var result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync(domainEvents);

        return result;
    }
}
