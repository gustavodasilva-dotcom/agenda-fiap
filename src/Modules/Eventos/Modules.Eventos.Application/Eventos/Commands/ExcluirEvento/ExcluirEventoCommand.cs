using Agenda.Common.Shared;
using MediatR;

namespace Agenda.Modules.Eventos.Application.Eventos.Commands.ExcluirEvento
{
    public sealed record ExcluirEventoCommand(int Id) : IRequest<Result<int, Error>>;
}
