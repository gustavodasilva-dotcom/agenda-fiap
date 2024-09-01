using Agenda.Common.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Agenda.WebUI.Models;

public class ContatoModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
    public string Nome { get; set; }  = string.Empty;

    [Required(ErrorMessage = "Telefone é obrigatório")]
    [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "DDD é obrigatório")]
    public DDDs? DDD { get; set; } = DDDs.SP;

    [Required(ErrorMessage = "E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "Endereço de e-mail inválido")]
    public string Email { get; set; } = string.Empty;
}
