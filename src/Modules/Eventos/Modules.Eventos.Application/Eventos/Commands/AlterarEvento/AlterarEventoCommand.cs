using Agenda.Common.Shared;
using Agenda.Modules.Eventos.Application.Contracts;
using MediatR;

namespace Agenda.Modules.Eventos.Application.Eventos.Commands.AlterarEvento
{
    public sealed record AlterarEventoCommand(int Id, EventoRequest Evento) :
        IRequest<Result<EventoResponse, Error>>;
}
