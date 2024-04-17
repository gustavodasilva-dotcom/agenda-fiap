using Agenda.FIAP.Api.Application.Contatos.Commands.AdicionarContatos;
using Agenda.FIAP.Api.Application.Contracts.Requests;
using Agenda.FIAP.Api.Constants;
using Carter;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Agenda.FIAP.Api.Endpoints;

public class AddContato : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/contatos", async (List<ContatoRequest> contatos, ISender sender) =>
        {

            try
            {
                var resultadoValidacoes = new List<ValidationResult>();

                foreach (var contato in contatos)
                {
                    if (!Validator.TryValidateObject(contato,
                                                     new ValidationContext(contato),
                                                     resultadoValidacoes,
                                                     validateAllProperties: true))
                    {
                        return Results.BadRequest(resultadoValidacoes);
                    }
                }
                var result = await sender.Send(new AdicionarContatosCommand(contatos));

                return Results.Created($"contatos", result);
            }
            catch
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        })
        .WithTags(Tags.Contatos);
    }
}