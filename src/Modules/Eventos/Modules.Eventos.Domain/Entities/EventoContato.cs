using Agenda.Common.Shared.Abstractions;

namespace Agenda.Modules.Eventos.Domain.Entities;

public class EventoContato : BaseEntity
{
    private EventoContato(int eventoId, int contatoId)
    {
        EventoId = eventoId;
        ContatoId = contatoId;
    }

    private EventoContato() : this(eventoId: 0, contatoId: 0)
    {
    }

    public int EventoId { get; private set; }

    public int ContatoId { get; private set; }

    public override IEnumerable<object> GetAtomicValues()
        => [EventoId, ContatoId];

    public static EventoContato CriarContato(int eventoId, int contatoId)
        => new(eventoId, contatoId);
}
