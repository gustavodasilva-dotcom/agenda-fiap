using MediatR;
using Modules.Eventos.Application.Contracts;

namespace Modules.Eventos.Application.Eventos.Queries.ObterEventos
{
    public sealed record ObterEventosQuery() : IRequest<IEnumerable<EventoResponse>>;
}
