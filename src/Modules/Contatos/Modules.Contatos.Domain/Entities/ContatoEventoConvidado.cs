using Agenda.Common.Shared.Abstractions;
using Agenda.Modules.Contatos.Domain.Enums;

namespace Agenda.Modules.Contatos.Domain;

public class ContatoEventoConvidado : BaseEntity
{
    private ContatoEventoConvidado(
        int contatoId,
        int eventoId,
        StatusAceiteEvento statusAceiteEvento)
    {
        ContatoId = contatoId;
        EventoId = eventoId;
        StatusAceiteEvento = statusAceiteEvento;
    }

    private ContatoEventoConvidado()
        : this(
            contatoId: 0,
            eventoId: 0,
            StatusAceiteEvento.NaoRespondido)
    {
    }

    public int ContatoId { get; set; }

    public int EventoId { get; set; }

    public StatusAceiteEvento StatusAceiteEvento { get; set; }

    public override IEnumerable<object> GetAtomicValues()
        => [ContatoId, EventoId];

    public static ContatoEventoConvidado CriarEvento(
        int contatoId,
        int eventoId,
        StatusAceiteEvento statusAceiteEvento = StatusAceiteEvento.NaoRespondido)
        => new(contatoId, eventoId, statusAceiteEvento);
}
