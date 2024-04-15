using Agenda.FIAP.Api.Application.Contatos.Queries.ObterContatos;
using Agenda.FIAP.Api.Constants;
using Carter;
using MediatR;

namespace Agenda.FIAP.Api.Endpoints;

public class GetContatos : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/contatos", async (ISender sender) =>
        {
            var command = new ObterContatosQuery();

            return Results.Ok(await sender.Send(command));
        })
        .WithTags(Tags.Contatos);
    }
}
