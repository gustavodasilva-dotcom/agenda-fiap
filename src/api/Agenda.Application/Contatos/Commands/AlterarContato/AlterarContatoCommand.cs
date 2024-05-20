using Agenda.Application.Contracts.Requests;
using Agenda.Application.Contracts.Responses;
using Agenda.Domain.Shared;
using MediatR;

namespace Agenda.Application.Contatos.Commands.AlterarContato;

public sealed record AlterarContatoCommand(int Id, ContatoRequest Contato)
    : IRequest<Result<ContatoResponse, Error>>;
