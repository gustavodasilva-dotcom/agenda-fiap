using MediatR;
using Agenda.Modules.Eventos.Application.Contracts;

namespace Agenda.Modules.Eventos.Application.Eventos.Queries.ObterEventos
{
    public sealed record ObterEventosQuery() : IRequest<IEnumerable<EventoResponse>>;
}
