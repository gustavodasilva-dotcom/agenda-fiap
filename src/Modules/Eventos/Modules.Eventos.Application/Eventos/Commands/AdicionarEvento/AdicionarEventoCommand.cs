using Agenda.Common.Shared;
using Modules.Eventos.Application.Contracts;
using MediatR;

namespace Modules.Eventos.Application.Eventos.Commands.AdicionarEvento
{
    public sealed record AdicionarEventoCommand(EventoRequest evento): 
        IRequest<Result<EventoResponse,Error>>;
}
