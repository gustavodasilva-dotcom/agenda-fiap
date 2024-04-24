using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Shared;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Commands.ExcluirContato;

internal sealed class ExcluirContatoCommandHandler :
    IRequestHandler<ExcluirContatoCommand, Result<int, Error>>
{
    private readonly IContatoRepository _contatoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ExcluirContatoCommandHandler(
        IContatoRepository contatoRepository,
        IUnitOfWork unitOfWork)
    {
        _contatoRepository = contatoRepository;
        _unitOfWork = unitOfWork;
    }

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
