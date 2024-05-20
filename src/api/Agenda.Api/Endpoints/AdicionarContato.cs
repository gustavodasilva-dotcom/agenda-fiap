using Agenda.Api.Constants;
using Agenda.Application.Contatos.Commands.AdicionarContatos;
using Agenda.Application.Contracts.Requests;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Endpoints;

public class AddContato : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/contatos", async ([FromBody] List<ContatoRequest> contatos, ISender sender) =>
        {
            var result = await sender.Send(new AdicionarContatosCommand(contatos));

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Created($"contatos", result.Value);
        })
        .WithTags(Tags.Contatos);
    }
}
