using Agenda.Common.Enums;
using Agenda.Modules.Contatos.Application.Contracts.Responses;
using MediatR;

namespace Agenda.Modules.Contatos.Application.Contatos.Queries.ObterContatos;

public sealed record ObterContatosQuery(DDDs DDD) : IRequest<IEnumerable<ContatoResponse>>;
