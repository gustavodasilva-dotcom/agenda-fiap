using Agenda.FIAP.Api.Application.Contracts.Requests;
using Agenda.FIAP.Api.Application.Contracts.Responses;
using Agenda.FIAP.Api.Domain.Shared;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Commands.AlterarContato;

public sealed record AlterarContatoCommand(int Id, ContatoRequest Contato)
    : IRequest<Result<ContatoResponse, Error>>;
