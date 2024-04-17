using Agenda.FIAP.Api.Application.Contracts.Responses;
using Agenda.FIAP.Api.Domain.Enums;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Queries.ObterContatos;

public sealed record ObterContatosQuery(DDD DDD) : IRequest<IEnumerable<ContatoResponse>>;
