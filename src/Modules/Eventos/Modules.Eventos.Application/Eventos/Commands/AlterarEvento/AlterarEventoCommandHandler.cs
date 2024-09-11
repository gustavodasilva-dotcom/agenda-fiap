using Agenda.Common.Shared.Abstractions;
using Agenda.Common.Shared;
using Agenda.Modules.Eventos.Application.Contracts;
using Agenda.Modules.Eventos.Domain.Abstractions;
using Agenda.Modules.Eventos.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Modules.Eventos.Application.Eventos.Commands.AlterarEvento;

internal sealed class AlterarEventoCommandHandler(
    IEventoRepository eventoRepository,
    [FromKeyedServices(nameof(Eventos))] IUnitOfWork unitOfWork)
    : IRequestHandler<AlterarEventoCommand, Result<EventoResponse, Error>>
{
    private readonly IEventoRepository _eventoRepository = eventoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<EventoResponse, Error>> Handle(
        AlterarEventoCommand request,
        CancellationToken cancellationToken)
    {
        request.Deconstruct(out int EventoId, out EventoRequest eventoRequest);

        var resultadoValidacoes = new List<ValidationResult>();

        if (!Validator.TryValidateObject(request.Evento,
                                         new ValidationContext(eventoRequest),
                                         resultadoValidacoes,
                                         validateAllProperties: true))
        {
            return new Error(
                "AlterarEvento.Validacoes",
                string.Join("\n ", resultadoValidacoes.Select(v => v.ErrorMessage).ToArray()));
        }

        var eventoAtualizar = _eventoRepository.Obter(c => c.Id == EventoId);
        if (eventoAtualizar is null)
        {
            return new Error(
                "AlterarEvento.EventoNaoEncontrado",
                "Não foi encontrado nenhum evento com o id informado");
        }

        eventoAtualizar.AtualizarEvento(
            eventoRequest.Nome,
            eventoRequest.DataEventoInicio,
            eventoRequest.DataEventoFinal);

        foreach (EventoContato contato in eventoAtualizar.Contatos.Where(contato =>
            !eventoRequest.ContatosIds.Any(contatoId => contatoId == contato.ContatoId)))
        {
            eventoAtualizar.RemoverContato(contato.ContatoId);
        }

        foreach (int contatoId in eventoRequest.ContatosIds)
        {
            Evento? eventoExistente = _eventoRepository.ObterEventoPorPeriodoEContato(
                contatoId,
                eventoRequest.DataEventoInicio,
                eventoRequest.DataEventoFinal);

            if (eventoExistente is not null && eventoExistente.Id != EventoId)
            {
                return new Error(
                    "AlterarEvento.ExiteEventoMesmoPeriodoParaContato",
                    "Já existe um evento cadastrado para o mesmo periodo e contato.");
            }

            eventoAtualizar.AdicionarContato(contatoId);
        }

        _eventoRepository.Alterar(eventoAtualizar);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = eventoAtualizar.Adapt<EventoResponse>();

        return response;
    }
}
