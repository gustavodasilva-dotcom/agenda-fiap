using Agenda.FIAP.Api.Application.Contracts.Responses;
using Agenda.FIAP.Api.Domain.Enums;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Commands.AlterarContato;

public sealed record AlterarContatoCommand(
    int Id,
    string Nome,
    string Telefone,
    string Email,
    DDD DDD) : IRequest<ContatoResponse>;
