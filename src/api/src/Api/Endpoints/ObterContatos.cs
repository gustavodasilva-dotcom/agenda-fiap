using Agenda.FIAP.Api.Application.Contatos.Queries.ObterContatos;
using Agenda.FIAP.Api.Constants;
using Agenda.FIAP.Api.Domain.Enums;
using Carter;
using MediatR;

namespace Agenda.FIAP.Api.Endpoints;

public class GetContatos : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/contatos/{ddd:int}", async (DDD ddd, ISender sender) =>
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

