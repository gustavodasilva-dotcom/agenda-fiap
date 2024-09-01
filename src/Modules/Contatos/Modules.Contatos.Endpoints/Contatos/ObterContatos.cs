using Agenda.Common.Helpers.EndpointInstaller;
using Agenda.Common.Shared.Enums;
using Agenda.Modules.Contatos.Application.Contatos.Queries.ObterContatos;
using Agenda.Modules.Contatos.Endpoints.Constants;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Agenda.Modules.Contatos.Endpoints.Contatos;

public class ObterContatos : IEndpointInstaller
{
    public void InstallEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ContatosRoutes.ObterContatos, async (
            DDDs ddd,
            ISender sender) =>
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
