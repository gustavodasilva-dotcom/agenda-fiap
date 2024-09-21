using Agenda.Modules.Notificacoes.Domain;
using Agenda.Modules.Notificacoes.Domain.Contracts;
using Agenda.Modules.Notificacoes.Infrastructure.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Agenda.Modules.Notificacoes.Infrastructure.Services;

public class SmtpService : ISmtpService
{
    private readonly SmtpOptions _options;
    private readonly SmtpClient _smtpClient;

    public SmtpService(IOptions<SmtpOptions> options)
    {
        _options = options.Value;

        _smtpClient = new SmtpClient();

        _smtpClient.Connect(
            host: _options.Host,
            port: _options.Port,
            options: _options.IsSecure
                ? MailKit.Security.SecureSocketOptions.StartTls
                : MailKit.Security.SecureSocketOptions.None);
    }

    private MimeMessage GetMimeMessage(Email email)
    {
        var message = new MimeMessage();

        message.From.Add(
            new MailboxAddress(
                _options.Sender.Name,
                _options.Sender.Mail));

        message.To.Add(
            new MailboxAddress(
                email.Destinatario.Nome,
                email.Destinatario.Email));

        message.Subject = email.Assunto;

        message.Body = new TextPart(TextFormat.Html)
        {
            Text = email.Corpo
        };

        return message;
    }

    public Task SendMail(Email email, CancellationToken cancellationToken = default)
        => _smtpClient.SendAsync(GetMimeMessage(email), cancellationToken);
}
