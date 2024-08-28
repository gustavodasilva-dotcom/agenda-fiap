using Agenda.Common.Shared;
using Agenda.Modules.Contatos.Domain.Abstractions;
using MediatR;

namespace Agenda.Modules.Contatos.Application.Contatos.Commands.ExcluirContato;

internal sealed class ExcluirContatoCommandHandler(
    IContatoRepository contatoRepository,
    IUnitOfWork unitOfWork) :
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

        _contatoRepository.Excluir(contato);

        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
