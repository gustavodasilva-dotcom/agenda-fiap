using Agenda.Modules.Contatos.Application.Contracts.Responses;
using MediatR;

namespace Modules.Contatos.Application.Contatos.Queries.ObterContatosPorIdsQuery;

public sealed record ObterContatosPorIdsQuery(List<int> Ids) : IRequest<IEnumerable<ContatoResponse>>;
