using Agenda.Common.Shared;
using Agenda.Modules.Contatos.Application.Contracts.Requests;
using Agenda.Modules.Contatos.Application.Contracts.Responses;
using MediatR;

namespace Agenda.Modules.Contatos.Application.Contatos.Commands.AdicionarContatos;

public sealed record AdicionarContatosCommand(List<ContatoRequest> Contatos)
    : IRequest<Result<List<ContatoResponse>, Error>>;
