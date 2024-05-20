using Agenda.Api.Constants;
using Agenda.Application.Contatos.Queries.ObterContatos;
using Agenda.Common.Enums;
using Carter;
using MediatR;

namespace Agenda.FIAP.Api.Endpoints;

public class GetContatos : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/contatos/{ddd:int}", async (DDDs ddd, ISender sender) =>
        {
            var command = new ObterContatosQuery(ddd);

            var result = await sender.Send(command);

            if (!result.Any())
            {
                return Results.NoContent();
            }
            
            return Results.Ok(result);
        })
        .WithTags(Tags.Contatos);
    }
}

