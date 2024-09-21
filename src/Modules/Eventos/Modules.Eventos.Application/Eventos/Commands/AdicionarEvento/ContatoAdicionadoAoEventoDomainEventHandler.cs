using Agenda.Modules.Eventos.Domain.DomainEvents;
using Agenda.Modules.Eventos.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Agenda.Modules.Eventos.Application.Eventos.Commands.AdicionarEvento;

public class ContatoAdicionadoAoEventoDomainEventHandler(IPublishEndpoint publishEndpoint)
    : INotificationHandler<ContatoAdicionadoAoEventoDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public Task Handle(
        ContatoAdicionadoAoEventoDomainEvent notification,
        CancellationToken cancellationToken)
        => _publishEndpoint.Publish(
            new ContatoAdicionadoAoEventoIntegrationEvent(
                notification.ContatoId,
                notification.EventoId,
                notification.NomeEvento,
                notification.DataInicioEvento,
                notification.DataFinalEvento),
            cancellationToken);
}
