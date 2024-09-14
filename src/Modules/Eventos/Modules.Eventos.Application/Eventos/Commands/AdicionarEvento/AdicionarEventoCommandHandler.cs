using Agenda.Common.Shared.Abstractions;
using Agenda.Common.Shared;
using Agenda.Modules.Eventos.Application.Contracts;
using Agenda.Modules.Eventos.Domain.Abstractions;
using Agenda.Modules.Eventos.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Modules.Eventos.Application.Eventos.Commands.AdicionarEvento;

internal sealed class AdicionarEventoCommandHandler(
    IMapper mapper,
    IEventoRepository eventoRepository,
    [FromKeyedServices(nameof(Eventos))] IUnitOfWork unitOfWork)
    : IRequestHandler<AdicionarEventoCommand, Result<EventoResponse, Error>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IEventoRepository _eventoRepository = eventoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<EventoResponse, Error>> Handle(
        AdicionarEventoCommand request,
        CancellationToken cancellationToken)
    {
        request.Deconstruct(out EventoRequest eventoRequest);

        var resultadoValidacoes = new List<ValidationResult>();

        if (!Validator.TryValidateObject(request.Evento,
                                         new ValidationContext(eventoRequest),
                                         resultadoValidacoes,
                                         validateAllProperties: true))
        {
            return new Error(
                "AdicionarEvento.Validacoes",
                string.Join("\n ", resultadoValidacoes.Select(v => v.ErrorMessage).ToArray()));
        }

        Evento evento = Evento.CriarEvento(
            eventoRequest.Nome,
            eventoRequest.DataEventoInicio,
            eventoRequest.DataEventoFinal);

        foreach (int contatoId in eventoRequest.ContatosIds)
        {
            Evento? eventoExistente = _eventoRepository.ObterEventoPorPeriodoEContato(
                contatoId,
                eventoRequest.DataEventoInicio,
                eventoRequest.DataEventoFinal);

            if (eventoExistente is not null)
            {
                return new Error(
                    "AdicionarEvento.ExiteEventoMesmoPeriodoParaContato",
                    "Já existe um evento cadastrado para o mesmo periodo e contato.");
            }

            evento.AdicionarContato(contatoId);
        }

        _eventoRepository.Adicionar(evento);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<EventoResponse>(evento);
    }
}
