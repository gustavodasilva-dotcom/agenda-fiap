using Agenda.Common.Shared;
using Agenda.Modules.Eventos.Application.Contracts;
using MediatR;

namespace Agenda.Modules.Eventos.Application.Eventos.Commands.AdicionarEvento
{
    public sealed record AdicionarEventoCommand(EventoRequest Evento) :
        IRequest<Result<EventoResponse, Error>>;
}
