using Agenda.Common.Helpers.EndpointInstaller;
using Agenda.Modules.Eventos.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Eventos.Application.Contracts;
using Modules.Eventos.Application.Eventos.Commands.AdicionarEvento;
using Modules.Eventos.Endpoints.Constants;

namespace Modules.Eventos.Endpoints.Eventos
{
    public class AdicionarEvento : IEndpointInstaller
    {
        public void InstallEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost(EventosRoutes.AdicionarEventos, async (
                [FromBody] EventoRequest evento,
                ISender sender) =>
            {
                var command = new AdicionarEventoCommand(evento);

                var result = await sender.Send(command);
                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Created($"eventos", result.Value);
            })
            .WithTags(Tags.Eventos);
        }
    }
}
