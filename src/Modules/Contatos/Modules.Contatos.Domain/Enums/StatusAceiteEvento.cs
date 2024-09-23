using System.ComponentModel;

namespace Agenda.Modules.Contatos.Domain.Enums;

public enum StatusAceiteEvento
{
    [Description("Não respondido")]
    NaoRespondido = 0,

    [Description("Aceito")]
    Aceito = 1,

    [Description("Rejeitado")]
    Rejeitado = 2
}
