using Agenda.FIAP.Api.Application.Contatos.Commands.ExcluirContato;
using Agenda.FIAP.Api.Constants;
using Carter;
using MediatR;

namespace Agenda.FIAP.Api.Endpoints;

public class ExcluirContato : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/contatos/{id:int}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new ExcluirContatoCommand(id));

            if (result.IsFailure)
            {
                return Results.NotFound(result.Error);
            }

            return Results.NoContent();
        })
        .WithTags(Tags.Contatos);
    }
}
