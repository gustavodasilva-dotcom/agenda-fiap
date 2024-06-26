using Agenda.Api.Constants;
using Agenda.Application.Contatos.Commands.AlterarContato;
using Agenda.Application.Contracts.Requests;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.FIAP.Api.Endpoints;

public class AlterarContato : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/contatos/{id:int}", async (
            int id,
            [FromBody] ContatoRequest request,
            ISender sender) =>
        {
            var command = new AlterarContatoCommand(id, request);

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        })
        .WithTags(Tags.Contatos);
    }
}
