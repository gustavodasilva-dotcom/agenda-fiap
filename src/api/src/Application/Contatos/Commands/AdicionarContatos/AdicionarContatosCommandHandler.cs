using Agenda.FIAP.Api.Application.Contracts.Responses;
using Agenda.FIAP.Api.Application.Errors;
using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Entities;
using Agenda.FIAP.Api.Domain.Shared;
using Mapster;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Commands.AdicionarContatos;

internal sealed class AdicionarContatosCommandHandler
    : IRequestHandler<AdicionarContatosCommand, Result<List<ContatoResponse>, Error>>
{
    private readonly IContatoRepository _contatoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdicionarContatosCommandHandler(
        IContatoRepository contatoRepository,
        IUnitOfWork unitOfWork)
    {
        _contatoRepository = contatoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<ContatoResponse>, Error>> Handle(
        AdicionarContatosCommand request,
        CancellationToken cancellationToken)
    {
        List<Contato> contatos = [];

        foreach (var contato in request.Contatos)
        {
            var contatoTelefoneExistente = _contatoRepository
                .ContatoExistenteComMesmoTelefone(contato.Telefone);

            if (contatoTelefoneExistente is not null)
            {
                return ApplicationErrors.ContatoExistenteComMesmoTelefone;
            }

            var contatoEmailExistente = _contatoRepository
                .ContatoExistenteComMesmoEmail(contato.Email);

            if (contatoEmailExistente is not null)
            {
                return ApplicationErrors.ContatoExistenteComMesmoEmail;
            }

            var novoContato = Contato.CriarContato(
                nome: contato.Nome,
                telefone: contato.Telefone,
                email: contato.Email,
                ddd: contato.DDD);

            if (contatos.Any(c => c.Equals(novoContato)))
            {
                return ApplicationErrors.NaoPermitidoCadastrarContatoRepetido;
            }

            contatos.Add(novoContato);
        }

        _contatoRepository.Adicionar(contatos);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = contatos.Adapt<List<ContatoResponse>>();

        return response;
    }
}
