using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Common.Shared.Abstractions;

public abstract class BaseDbContext<TDbContext>(
    DbContextOptions<TDbContext> options,
    IPublisher publisher) : DbContext(options)
    where TDbContext : DbContext
{
    private readonly IPublisher _publisher = publisher;

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<BaseEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvents;
            });

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync();

        return result;
    }
}
