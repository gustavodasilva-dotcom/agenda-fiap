using Agenda.FIAP.Api.Application.Contracts.Requests;
using Agenda.FIAP.Api.Application.Contracts.Responses;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Commands.AdicionarContatos;

public sealed record AdicionarContatosCommand(List<ContatoRequest> Contatos)
    : IRequest<IEnumerable<ContatoResponse>>;
