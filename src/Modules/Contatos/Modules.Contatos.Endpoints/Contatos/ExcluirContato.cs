using Agenda.Common.Helpers.EndpointInstaller;
using Agenda.Modules.Contatos.Application.Contatos.Commands.ExcluirContato;
using Agenda.Modules.Contatos.Endpoints.Constants;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Agenda.Modules.Contatos.Endpoints.Contatos;

public class ExcluirContatos : IEndpointInstaller
{
    public void InstallEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(ContatosRoutes.ExcluirContato, async (
            int id,
            ISender sender) =>
        {
            var command = new ExcluirContatoCommand(id);

            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return Results.NotFound(result.Error);
            }

            return Results.NoContent();
        })
        .WithTags(Tags.Contatos);
    }
}
