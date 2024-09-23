namespace Agenda.Common.Shared.Abstractions;

public abstract class DomainEvent
{
    private readonly List<IDomainEvent> _domainEvents = [];

    private readonly List<Func<IDomainEvent>> _eventFactories = [];

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
        => [.. _domainEvents];

    public IReadOnlyList<IDomainEvent> GetEventFactories()
        => [.. _eventFactories.Select(factory => factory())];

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void ClearEventFactories() => _eventFactories.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    protected void RaiseEventFactory(Func<IDomainEvent> action)
        => _eventFactories.Add(action);
}
