using Agenda.FIAP.Api.Constants;
using Carter;

namespace Agenda.FIAP.Api.Endpoints;

public class AddContato : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("contatos", () =>
        {
            return Results.Created();
        })
        .WithTags(Tags.Contatos);
    }
}
