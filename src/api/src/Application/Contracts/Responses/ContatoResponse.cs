﻿using Domain.Enum;

namespace Agenda.FIAP.Api.Application.Contracts.Responses;

public class ContatoResponse
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Telefone { get; set; }

    public string Email { get; set; }
    
    public DDD Ddd { get; set; }
}