using Agenda.Common.Helpers.EndpointInstaller;
using Agenda.Modules.Eventos.Application.Eventos.Commands.ExcluirEvento;
using Agenda.Modules.Eventos.Endpoints.Constants;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Agenda.Modules.Eventos.Endpoints.Eventos
{
    public class ExcluirEvento : IEndpointInstaller
    {
        public void InstallEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete(EventosRoutes.ExcluirEvento, async (
                int id,
                ISender sender) =>
            {
                var command = new ExcluirEventoCommand(id);

                var result = await sender.Send(command);
                if (result.IsFailure)
                {
                    return Results.NotFound(result.Error);
                }

                return Results.NoContent();
            })
            .WithTags(Tags.Eventos);
        }
    }
}
