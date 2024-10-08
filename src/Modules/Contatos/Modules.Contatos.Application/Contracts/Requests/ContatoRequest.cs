﻿using Agenda.Common.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Modules.Contatos.Application.Contracts.Requests;

public class ContatoRequest
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O telefone é obrigatório")]
    [Length(8, 9, ErrorMessage = "Numero do Telefone deve possuir 8 ou 9 caracteres")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
    public string Email { get; set; } = string.Empty;

    [EnumDataType(typeof(DDDs), ErrorMessage = "DDD inválido.")]
    public DDDs DDD { get; set; }
}
