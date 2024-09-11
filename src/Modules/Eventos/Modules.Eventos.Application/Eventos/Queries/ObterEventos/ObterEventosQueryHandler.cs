using Agenda.Modules.Eventos.Application.Contracts;
using Agenda.Modules.Eventos.Domain.Abstractions;
using Agenda.Modules.Eventos.Domain.Entities;
using Mapster;
using MediatR;

namespace Agenda.Modules.Eventos.Application.Eventos.Queries.ObterEventos;

internal sealed class ObterEventosQueryHandler(IEventoRepository eventoRepository)
    : IRequestHandler<ObterEventosQuery, IEnumerable<EventoResponse>>
{
    private readonly IEventoRepository _eventoRepository = eventoRepository;

    public Task<IEnumerable<EventoResponse>> Handle(
        ObterEventosQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Evento> eventos = _eventoRepository.ObterTodos();

        var response = eventos.Adapt<IEnumerable<EventoResponse>>();

        return Task.FromResult(response);
    }
}
