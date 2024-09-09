namespace Agenda.Common.Shared.Abstractions;

public abstract class DomainEvent
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
        => [.. _domainEvents];

    public void ClearDomainEvents()
       => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);
}
