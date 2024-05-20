using Agenda.Common.Enums;

namespace Agenda.Application.Contracts.Responses;

public class ContatoResponse
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    
    public DDDs DDD { get; set; }
}
