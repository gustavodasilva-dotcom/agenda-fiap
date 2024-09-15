namespace Agenda.Modules.Notificacoes.Infrastructure.Options;

public sealed class SmtpOptions
{
    public const string Position = "Smtp";

    public SmtpEntityOptions Sender { get; set; } = default!;

    public string Host { get; set; } = string.Empty;

    public int Port { get; set; }

    public bool IsSecure { get; set; }
}
