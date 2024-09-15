namespace Agenda.Modules.Notificacoes.Domain.Contracts;

public record Email(EntidadeEmail Destinatario, string Assunto, string Corpo);
