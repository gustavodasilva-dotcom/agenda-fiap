using Agenda.Modules.Eventos.Application.Eventos.Queries.ObterEventos;
using Agenda.Modules.Eventos.Endpoints;
using Agenda.Modules.Eventos.Endpoints.Constants;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Agenda.Common.Helpers.EndpointInstaller;

namespace Modules.Eventos.Endpoints.Eventos
{
    public class ObterEvento : IEndpointInstaller
    {
        public void InstallEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet(EventosRoutes.ObterEventos, async (
                ISender sender) =>
            {
                var command = new ObterEventosQuery();

                var result = await sender.Send(command);
                if (!result.Any())
                {
                    return Results.NoContent();
                }

                return Results.Ok(result);
            })
            .WithTags(Tags.Eventos);
        }
    }
}
