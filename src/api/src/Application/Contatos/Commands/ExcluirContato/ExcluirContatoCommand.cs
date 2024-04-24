using Agenda.FIAP.Api.Domain.Shared;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Commands.ExcluirContato;

public sealed record ExcluirContatoCommand(int Id) : IRequest<Result<int, Error>>;
