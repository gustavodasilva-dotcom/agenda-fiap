using Agenda.Common.Helpers.EndpointInstaller;
using Agenda.Modules.Contatos.Endpoints.Constants;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Contatos.Application.Contatos.Queries.ObterContatosPorIdsQuery;

namespace Agenda.Modules.Contatos.Endpoints.Contatos;

public class ObterContatosPorIds : IEndpointInstaller
{
    public void InstallEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ContatosRoutes.ObterContatosPorIds, async (
            HttpContext context,
            ISender sender) =>
        {
            var idsString = context.Request.Query["ids"].ToString();

            if (string.IsNullOrEmpty(idsString))
            {
                return Results.BadRequest("Nenhum ID foi fornecido.");
            }

            var ids = idsString.Split(',').Select(int.Parse).ToList();

            var command = new ObterContatosPorIdsQuery(ids);
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
