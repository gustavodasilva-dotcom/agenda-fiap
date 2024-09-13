using Agenda.Common.Helpers.EndpointInstaller;
using Agenda.Modules.Eventos.Application.Contracts;
using Agenda.Modules.Eventos.Application.Eventos.Commands.AlterarEvento;
using Agenda.Modules.Eventos.Endpoints.Constants;
using Agenda.Modules.Eventos.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Modules.Eventos.Endpoints.Eventos;

public class AlterarEvento : IEndpointInstaller
{
    public void InstallEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(EventosRoutes.AlterarEventos, async (
            int id,
            [FromBody] EventoRequest request,
            ISender sender) =>
        {
            var command = new AlterarEventoCommand(id, request);

            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        })
        .WithTags(Tags.Eventos);
    }
}
