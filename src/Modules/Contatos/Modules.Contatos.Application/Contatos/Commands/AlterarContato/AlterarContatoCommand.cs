using Agenda.Common.Shared;
using Agenda.Modules.Contatos.Application.Contracts.Requests;
using Agenda.Modules.Contatos.Application.Contracts.Responses;
using MediatR;

namespace Agenda.Modules.Contatos.Application.Contatos.Commands.AlterarContato;

public sealed record AlterarContatoCommand(int Id, ContatoRequest Contato)
    : IRequest<Result<ContatoResponse, Error>>;
