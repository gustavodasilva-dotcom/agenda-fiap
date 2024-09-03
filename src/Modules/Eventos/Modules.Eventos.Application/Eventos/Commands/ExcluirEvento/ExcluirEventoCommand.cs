using Agenda.Common.Shared;
using MediatR;

namespace Modules.Eventos.Application.Eventos.Commands.ExcluirEvento
{
    public sealed record ExcluirEventoCommand(int Id) : IRequest<Result<int,Error>>;
}
