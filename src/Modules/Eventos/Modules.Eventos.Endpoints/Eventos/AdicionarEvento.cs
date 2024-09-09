using Agenda.Common.Helpers.EndpointInstaller;
using Agenda.Modules.Eventos.Application.Contracts;
using Agenda.Modules.Eventos.Application.Eventos.Commands.AdicionarEvento;
using Agenda.Modules.Eventos.Endpoints.Constants;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Agenda.Modules.Eventos.Endpoints.Eventos
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
