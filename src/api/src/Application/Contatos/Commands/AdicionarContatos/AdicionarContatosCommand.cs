using Agenda.FIAP.Api.Application.Contracts.Requests;
using Agenda.FIAP.Api.Application.Contracts.Responses;
using Agenda.FIAP.Api.Domain.Shared;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Commands.AdicionarContatos;

public sealed record AdicionarContatosCommand(List<ContatoRequest> Contatos)
    : IRequest<Result<List<ContatoResponse>, Error>>;
