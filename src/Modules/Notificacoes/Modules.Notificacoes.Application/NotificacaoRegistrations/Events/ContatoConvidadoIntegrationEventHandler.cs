using Agenda.Common.Shared.Extensions;
using Agenda.Modules.Contatos.IntegrationEvents;
using Agenda.Modules.Notificacoes.Domain;
using Agenda.Modules.Notificacoes.Domain.Contracts;
using HtmlAgilityPack;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Agenda.Modules.Notificacoes.Application.NotificacaoRegistrations.Events;

public class ContatoConvidadoIntegrationEventHandler(IHostEnvironment env, ISmtpService smtpService)
    : IConsumer<ContatoConvidadoIntegrationEvent>
{
    private readonly IHostEnvironment _env = env;
    private readonly ISmtpService _smtpService = smtpService;

    public Task Consume(ConsumeContext<ContatoConvidadoIntegrationEvent> context)
    {
        var pastaTemplates = _env.ContentRootPath.ConcatenarCaminho(["Contents", "Templates"]);
        var arquivoTemplateEmail = Path.Combine(pastaTemplates, "convite-email-template.html");

        var documentHtml = new HtmlDocument();
        documentHtml.LoadHtml(File.ReadAllText(arquivoTemplateEmail));

        HtmlNode? nodeNomeContato = documentHtml
            .DocumentNode
            .SelectSingleNode("//*[@id='nome-contato']");

        if (nodeNomeContato is not null)
        {
            nodeNomeContato.InnerHtml = context.Message.NomeContato;
        }

        HtmlNode? nodeNomeEvento = documentHtml
            .DocumentNode
            .SelectSingleNode("//*[@id='nome-evento']");

        if (nodeNomeEvento is not null)
        {
            nodeNomeEvento.InnerHtml = context.Message.NomeEvento;
        }

        HtmlNode? nodeDataInicio = documentHtml
            .DocumentNode
            .SelectSingleNode("//*[@id='data-inicio-evento']");

        if (nodeDataInicio is not null)
        {
            nodeDataInicio.InnerHtml = context.Message.DataInicioEvento.ToString();
        }

        HtmlNode? nodeDataFim = documentHtml
            .DocumentNode
            .SelectSingleNode("//*[@id='data-fim-evento']");

        if (nodeDataFim is not null)
        {
            nodeDataFim.InnerHtml = context.Message.DataFinalEvento.ToString();
        }

        HtmlNode? nodeAnoAtual = documentHtml
            .DocumentNode
            .SelectSingleNode("//*[@id='ano-atual']");

        if (nodeAnoAtual is not null)
        {
            nodeAnoAtual.InnerHtml = DateTime.Now.Year.ToString();
        }

        return _smtpService.SendMail(
            new Email(
                Destinatario: new EntidadeEmail(
                    context.Message.NomeContato,
                    context.Message.EmailContato),
                Assunto: $"Você foi convidado(a) para o evento {context.Message.NomeEvento}",
                Corpo: documentHtml.DocumentNode.OuterHtml),
            context.CancellationToken);
    }
}
