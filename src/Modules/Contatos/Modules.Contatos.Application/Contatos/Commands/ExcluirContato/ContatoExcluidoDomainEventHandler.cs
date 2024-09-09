using Agenda.Modules.Contatos.Domain.DomainEvents;
using Agenda.Modules.Contatos.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Agenda.Modules.Contatos.Application.Contatos.Commands.ExcluirContato;

public class ContatoExcluidoDomainEventHandler(IPublishEndpoint publishEndpoint)
    : INotificationHandler<ContatoExcluidoDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public Task Handle(
        ContatoExcluidoDomainEvent notification,
        CancellationToken cancellationToken)
        => _publishEndpoint.Publish(
            new ContatoExcluidoIntegrationEvent(notification.ContatoId),
            cancellationToken);
}
