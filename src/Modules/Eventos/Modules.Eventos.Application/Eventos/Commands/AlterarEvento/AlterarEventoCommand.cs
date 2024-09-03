using Agenda.Common.Shared;
using MediatR;
using Modules.Eventos.Application.Contracts;

namespace Modules.Eventos.Application.Eventos.Commands.AlterarEvento
{
    public sealed record AlterarEventoCommand(int Id, EventoRequest Evento): 
        IRequest<Result<EventoResponse,Error>>;
}
