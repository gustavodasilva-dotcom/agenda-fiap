using Agenda.FIAP.Api.Domain.Entities;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Queries.GetContatos;

public sealed record GetContatosQuery : IRequest<IEnumerable<Contato>>;
