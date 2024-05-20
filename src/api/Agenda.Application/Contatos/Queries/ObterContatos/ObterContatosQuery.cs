using Agenda.Application.Contracts.Responses;
using Agenda.Common.Enums;
using MediatR;

namespace Agenda.Application.Contatos.Queries.ObterContatos;

public sealed record ObterContatosQuery(DDDs DDD) : IRequest<IEnumerable<ContatoResponse>>;
