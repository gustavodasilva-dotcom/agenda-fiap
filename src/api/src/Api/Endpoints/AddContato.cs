using Agenda.FIAP.Api.Constants;
using Agenda.FIAP.Api.Domain.Entities;
using Carter;
using Infrastructure.Data.Context;

namespace Agenda.FIAP.Api.Endpoints;

public class AddContato : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("contatos", async (List<Contato> contatos, DataContext dbContext) =>
        {
            await dbContext.AddRangeAsync(contatos);
            await dbContext.SaveChangesAsync();

            return Results.Created($"contatos", contatos);
        }).WithTags(Tags.Contatos);
    }
}
