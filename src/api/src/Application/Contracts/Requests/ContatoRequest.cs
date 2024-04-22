using Agenda.FIAP.Api.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Agenda.FIAP.Api.Application.Contracts.Requests;

public class ContatoRequest
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O telefone é obrigatório")]
    [Length(8, 9, ErrorMessage = "Numero do Telefone deve possuir 8 ou 9 caracteres")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
    public string Email { get; set; }

    [EnumDataType(typeof(DDD), ErrorMessage = "DDD inválido.")]
    public DDD DDD { get; set; }
}
