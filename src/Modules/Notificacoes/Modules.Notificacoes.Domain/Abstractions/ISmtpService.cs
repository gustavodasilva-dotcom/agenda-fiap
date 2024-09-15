using Agenda.Modules.Notificacoes.Domain.Contracts;

namespace Agenda.Modules.Notificacoes.Domain;

public interface ISmtpService
{
    Task SendMail(Email email, CancellationToken cancellationToken);
}
