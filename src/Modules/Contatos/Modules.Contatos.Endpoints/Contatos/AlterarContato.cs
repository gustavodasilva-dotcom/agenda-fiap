using Agenda.Common.Helpers.EndpointInstaller;
using Agenda.Modules.Contatos.Application.Contatos.Commands.AlterarContato;
using Agenda.Modules.Contatos.Application.Contracts.Requests;
using Agenda.Modules.Contatos.Endpoints.Constants;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Agenda.Modules.Contatos.Endpoints.Contatos;

public class AlterarContatos : IEndpointInstaller
{
    public void InstallEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(ContatosRoutes.AlterarContatos, async (
            int id,
            [FromBody] ContatoRequest request,
            ISender sender) =>
        {
            var command = new AlterarContatoCommand(id, request);

            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        })
        .WithTags(Tags.Contatos);
    }
}
