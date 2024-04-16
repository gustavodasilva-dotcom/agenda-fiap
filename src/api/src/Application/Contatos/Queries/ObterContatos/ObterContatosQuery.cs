using Agenda.FIAP.Api.Application.Contracts.Responses;
using Application.Contracts.Requests;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Queries.ObterContatos;

public sealed record ObterContatosQuery(ContatoFiltroRequest filtro) 
    : IRequest<IEnumerable<ContatoResponse>>;
