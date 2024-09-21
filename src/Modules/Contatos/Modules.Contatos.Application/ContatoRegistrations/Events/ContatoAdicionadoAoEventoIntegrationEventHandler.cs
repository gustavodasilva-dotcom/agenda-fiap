using Agenda.Common.Shared.Abstractions;
using Agenda.Modules.Contatos.Domain.Abstractions;
using Agenda.Modules.Contatos.IntegrationEvents;
using Agenda.Modules.Eventos.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Contatos.Application.ContatoRegistrations.Events;

public class ContatoAdicionadoAoEventoIntegrationEventHandler(
    IPublishEndpoint publishEndpoint,
    IContatoRepository contatoRepository,
    [FromKeyedServices(nameof(Contatos))] IUnitOfWork unitOfWork)
    : IConsumer<ContatoAdicionadoAoEventoIntegrationEvent>
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly IContatoRepository _contatoRepository = contatoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task Consume(ConsumeContext<ContatoAdicionadoAoEventoIntegrationEvent> context)
    {
        var contato = _contatoRepository.Obter(c => c.Id == context.Message.ContatoId);
        if (contato is not null)
        {
            contato.AdicionarEventoConvidado(context.Message.EventoId);

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);

            await _publishEndpoint.Publish(
                new ContatoConvidadoIntegrationEvent(
                    contato.Nome,
                    contato.Email,
                    context.Message.NomeEvento,
                    context.Message.DataInicioEvento,
                    context.Message.DataFinalEvento),
                context.CancellationToken);
        }
    }
}
