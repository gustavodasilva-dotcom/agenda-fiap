using Agenda.Common.Helpers.EndpointInstaller;
using Agenda.Modules.Contatos.Application.Contatos.Commands.AdicionarContatos;
using Agenda.Modules.Contatos.Application.Contracts.Requests;
using Agenda.Modules.Contatos.Endpoints.Constants;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Agenda.Modules.Contatos.Endpoints.Contatos;

public class AdicionarContatos : IEndpointInstaller
{
    public void InstallEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ContatosRoutes.AdicionarContatos, async (
            [FromBody] List<ContatoRequest> contatos,
            ISender sender) =>
        {
            var command = new AdicionarContatosCommand(contatos);

            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Created($"contatos", result.Value);
        })
        .WithTags(Tags.Contatos);
    }
}
