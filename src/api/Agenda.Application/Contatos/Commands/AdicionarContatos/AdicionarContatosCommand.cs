using Agenda.Application.Contracts.Requests;
using Agenda.Application.Contracts.Responses;
using Agenda.Domain.Shared;
using MediatR;

namespace Agenda.Application.Contatos.Commands.AdicionarContatos;

public sealed record AdicionarContatosCommand(List<ContatoRequest> Contatos)
    : IRequest<Result<List<ContatoResponse>, Error>>;
