using Agenda.Common.Shared.Abstractions;
using Agenda.Common.Shared;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using Agenda.Modules.Eventos.Domain.Abstractions;
using Modules.Eventos.Application.Contracts;
using Agenda.Modules.Eventos.Domain.Entities;
using Mapster;

namespace Modules.Eventos.Application.Eventos.Commands.AlterarEvento
{
    public class AlterarEventoCommandHandler(
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
            var resultadoValidacoes = new List<ValidationResult>();

            if (!Validator.TryValidateObject(request.Evento,
                                             new ValidationContext(request.Evento),
                                             resultadoValidacoes,
                                             validateAllProperties: true))
            {
                return new Error(
                    "AlterarEvento.Validacoes",
                    string.Join("\n ", resultadoValidacoes.Select(v => v.ErrorMessage).ToArray()));
            }

            EventoRequest eventoRequest = request.Evento;
            Evento eventoExistente = _eventoRepository
                .ObterEventoPorPeriodoEContato(eventoRequest.IdContato, eventoRequest.DataEventoInicio, eventoRequest.DataEventoFinal );

            if (eventoExistente is not null
                && eventoExistente.Contatos.Any(x => x.Id != eventoRequest.IdContato))
            {
                return new Error(
                    "AlterarEvento.ExiteEventoMesmoPeriodoParaContato",
                    "Já existe um evento cadastrado para o mesmo periodo e contato.");
            }

            var eventoAtualizar = _eventoRepository.Obter(c => c.Id == request.Id);

            if (eventoAtualizar is null)
            {
                return new Error(
                    "AlterarEvento.EventoNaoEncontrado",
                    "Não foi encontrado nenhum evento com o id informado");
            }

            eventoAtualizar.AtualizarEvento(
                nome: eventoRequest.Nome,
                dataEventoInicio: eventoRequest.DataEventoInicio,
                dataEventoFinal: eventoRequest.DataEventoFinal);

            _eventoRepository.Alterar(eventoAtualizar);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = eventoAtualizar.Adapt<EventoResponse>();

            return response;
        }
    }
}
