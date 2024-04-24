using Agenda.FIAP.Api.Application.Contatos.Commands.AdicionarContatos;
using Agenda.FIAP.Api.Application.Contracts.Requests;
using Agenda.FIAP.Api.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.FIAP.Api.Endpoints;

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
