using Agenda.FIAP.Api.Application.Contracts.Responses;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Queries.ObterContatos;

public sealed record ObterContatosQuery : IRequest<IEnumerable<ContatoResponse>>;
