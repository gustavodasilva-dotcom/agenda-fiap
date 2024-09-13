using Agenda.Modules.Eventos.Application.Contracts;
using Agenda.Modules.Eventos.Domain.Abstractions;
using Agenda.Modules.Eventos.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Agenda.Modules.Eventos.Application.Eventos.Queries.ObterEventos;

internal sealed class ObterEventosQueryHandler(IMapper mapper, IEventoRepository eventoRepository)
    : IRequestHandler<ObterEventosQuery, IEnumerable<EventoResponse>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IEventoRepository _eventoRepository = eventoRepository;

    public Task<IEnumerable<EventoResponse>> Handle(
        ObterEventosQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Evento> eventos = _eventoRepository.ObterTodos();

        var response = _mapper.Map<IEnumerable<EventoResponse>>(eventos);

        return Task.FromResult(response);
    }
}
