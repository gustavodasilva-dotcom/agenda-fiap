using Agenda.Common.Shared;
using MediatR;

namespace Agenda.Modules.Contatos.Application.Contatos.Commands.ExcluirContato;

public sealed record ExcluirContatoCommand(int Id) : IRequest<Result<int, Error>>;
