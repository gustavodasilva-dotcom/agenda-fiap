using Agenda.FIAP.Api.Application.Contatos.Commands.AdicionarContatos;
using Agenda.FIAP.Api.Application.Contracts.Requests;
using Agenda.FIAP.Api.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Agenda.FIAP.Api.Endpoints;

public class AddContato : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/contatos", async ([FromBody] List<ContatoRequest> contatos, ISender sender) =>
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
        })
        .WithTags(Tags.Contatos);
    }
}
