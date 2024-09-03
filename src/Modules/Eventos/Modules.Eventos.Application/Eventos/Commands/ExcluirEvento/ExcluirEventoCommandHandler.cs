using Agenda.Common.Shared.Abstractions;
using Agenda.Common.Shared;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Agenda.Modules.Eventos.Domain.Abstractions;

namespace Modules.Eventos.Application.Eventos.Commands.ExcluirEvento
{
    public class ExcluirEventoCommandHandler(
        IEventoRepository eventoRepository,
        [FromKeyedServices(nameof(Eventos))] IUnitOfWork unitOfWork) :
        IRequestHandler<ExcluirEventoCommand, Result<int, Error>>
    {
        private readonly IEventoRepository _eventoRepository = eventoRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<int, Error>> Handle(
            ExcluirEventoCommand request,
            CancellationToken cancellationToken)
        {
            var contato = _eventoRepository.Obter(c => c.Id == request.Id);

            if (contato is null)
            {
                return new Error(
                    "ExcluirEvento.EventoNaoEncontrado",
                    "Não foi encontrado nenhum evento com o id informado");
            }

            _eventoRepository.Excluir(contato);

            return await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
