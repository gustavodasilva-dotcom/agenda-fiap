using Agenda.FIAP.Api.Application.Contracts.Responses;
using Agenda.FIAP.Api.Application.Errors;
using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Shared;
using Mapster;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Commands.AlterarContato;

internal sealed class AlterarContatoCommandHandler
    : IRequestHandler<AlterarContatoCommand, Result<ContatoResponse, Error>>
{
    private readonly IContatoRepository _contatoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AlterarContatoCommandHandler(
        IContatoRepository contatoRepository,
        IUnitOfWork unitOfWork)
    {
        _contatoRepository = contatoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ContatoResponse, Error>> Handle(
        AlterarContatoCommand request,
        CancellationToken cancellationToken)
    {
        var contatoTelefoneExistente = _contatoRepository
            .ContatoExistenteComMesmoTelefone(request.Telefone);

        if (contatoTelefoneExistente is not null &&
            contatoTelefoneExistente.Id != request.Id)
        {
            return ApplicationErrors.ContatoExistenteComMesmoTelefone;
        }

        var contatoEmailExistente = _contatoRepository
            .ContatoExistenteComMesmoEmail(request.Email);

        if (contatoEmailExistente is not null &&
            contatoEmailExistente.Id != request.Id)
        {
            return ApplicationErrors.ContatoExistenteComMesmoEmail;
        }

        var contatoAtualizar = _contatoRepository.Obter(c => c.Id == request.Id);

        if (contatoAtualizar is null)
        {
            return ApplicationErrors.ContatoNaoEncontrado;
        }

        contatoAtualizar.AtualizarContato(
            nome: request.Nome,
            telefone: request.Telefone,
            email: request.Email,
            ddd: request.DDD);

        _contatoRepository.Alterar(contatoAtualizar);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = contatoAtualizar.Adapt<ContatoResponse>();

        return response;
    }
}
