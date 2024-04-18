using System.ComponentModel.DataAnnotations;
using Agenda.FIAP.Api.Application.Contatos.Commands.AlterarContato;
using Agenda.FIAP.Api.Application.Contracts.Requests;
using Agenda.FIAP.Api.Constants;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.FIAP.Api.Endpoints;

public class AlterarContato : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/contatos/{id:int}", async (
            int id,
            [FromBody] ContatoRequest request,
            ISender sender) =>
        {
            var resultadoValidacoes = new List<ValidationResult>();

            if (!Validator.TryValidateObject(request,
                                             new ValidationContext(request),
                                             resultadoValidacoes,
                                             validateAllProperties: true))
            {
                return Results.BadRequest(resultadoValidacoes);
            }

            var command = new AlterarContatoCommand(
                Id: id,
                Nome: request.Nome,
                Telefone: request.Telefone,
                Email: request.Email,
                DDD: request.DDD);

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
