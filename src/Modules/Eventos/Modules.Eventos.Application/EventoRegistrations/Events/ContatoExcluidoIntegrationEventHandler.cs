using Agenda.Common.Shared.Abstractions;
using Agenda.Modules.Contatos.IntegrationEvents;
using Agenda.Modules.Eventos.Domain.Abstractions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Eventos.Application.EventoRegistrations.Events;

public class ContatoExcluidoIntegrationEventHandler(
    IEventoRepository eventoRepository,
    [FromKeyedServices(nameof(Eventos))] IUnitOfWork unitOfWork)
    : IConsumer<ContatoExcluidoIntegrationEvent>
{
    private readonly IEventoRepository _eventoRepository = eventoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Consume(
        ConsumeContext<ContatoExcluidoIntegrationEvent> context)
    {
        ContatoExcluidoIntegrationEvent message = context.Message;

        var eventos = _eventoRepository
            .ObterEventosFuturosDoContato(message.ContatoId);

        if (eventos.Count != 0)
        {
            eventos.ForEach(
                evento => evento.RemoverContato(message.ContatoId));
        }

        await _unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
