using Agenda.Common.Shared;
using Agenda.Common.Shared.Abstractions;
using Agenda.Modules.Contatos.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Contatos.Application.Contatos.Commands.ExcluirContato;

internal sealed class ExcluirContatoCommandHandler(
    IContatoRepository contatoRepository,
    [FromKeyedServices(nameof(Contatos))] IUnitOfWork unitOfWork) :
    IRequestHandler<ExcluirContatoCommand, Result<int, Error>>
{
    private readonly IContatoRepository _contatoRepository = contatoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<int, Error>> Handle(
        ExcluirContatoCommand request,
        CancellationToken cancellationToken)
    {
        var contato = _contatoRepository.Obter(c => c.Id == request.Id);

        if (contato is null)
        {
            return new Error(
                "ExcluirContato.ContatoNaoEncontrado",
                "Não foi encontrado nenhum contato com o id informado");
        }

        contato.RaiseContatoExcluidoDomainEvent();

        _contatoRepository.Excluir(contato);

        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
