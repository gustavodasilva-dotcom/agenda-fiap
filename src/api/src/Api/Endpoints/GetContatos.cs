using Agenda.FIAP.Api.Application.Contatos.Queries.ObterContatos;
using Agenda.FIAP.Api.Constants;
using Application.Contracts.Requests;
using Carter;
using MediatR;

namespace Agenda.FIAP.Api.Endpoints;

public class GetContatos : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/contatos", async (ContatoFiltroRequest filtro, ISender sender) =>
        {
            try
            {
                var command = new ObterContatosQuery(filtro);
                var result = await sender.Send(command);
                return Results.Ok(result);
            }
            catch
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        })
        .WithTags(Tags.Contatos);
    }
}

