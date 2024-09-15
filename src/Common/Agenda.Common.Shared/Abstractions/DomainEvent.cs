namespace Agenda.Common.Shared.Abstractions;

public abstract class DomainEvent
{
    private readonly List<IDomainEvent> _domainEvents = [];

    private readonly List<Func<IDomainEvent>> _eventFactories = [];

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        _domainEvents.AddRange(
            [.. _eventFactories.Select(factory => factory())]);

        return [.. _domainEvents];
    }

    public void ClearDomainEvents()
       => _eventFactories.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    protected void RaiseDomainEvent(Func<IDomainEvent> action)
        => _eventFactories.Add(action);
}
