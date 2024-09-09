using Agenda.Modules.Eventos.Domain.Abstractions;
using Agenda.Modules.Eventos.Domain.Entities;
using Mapster;
using MediatR;
using Agenda.Modules.Eventos.Application.Contracts;

namespace Agenda.Modules.Eventos.Application.Eventos.Queries.ObterEventos
{
    public class ObterEventosQueryHandler : IRequestHandler<ObterEventosQuery, IEnumerable<EventoResponse>>
    {
        private readonly IEventoRepository _eventoRepository;

        public ObterEventosQueryHandler(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public Task<IEnumerable<EventoResponse>> Handle(
            ObterEventosQuery request,
            CancellationToken cancellationToken)
        {
            IEnumerable<Evento> eventos = _eventoRepository.ObterTodos();

            var response = eventos.Adapt<IEnumerable<EventoResponse>>();

            return Task.FromResult(response);
        }
    }
}
