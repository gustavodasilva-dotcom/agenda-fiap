using Agenda.Domain.Shared;
using MediatR;

namespace Agenda.Application.Contatos.Commands.ExcluirContato;

public sealed record ExcluirContatoCommand(int Id) : IRequest<Result<int, Error>>;
