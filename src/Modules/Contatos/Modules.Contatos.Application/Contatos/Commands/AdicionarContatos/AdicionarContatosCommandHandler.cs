using Agenda.Common.Shared;
using Agenda.Modules.Contatos.Application.Contracts.Responses;
using Agenda.Modules.Contatos.Domain.Abstractions;
using Agenda.Modules.Contatos.Domain.Entities;
using Mapster;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Modules.Contatos.Application.Contatos.Commands.AdicionarContatos;

internal sealed class AdicionarContatosCommandHandler(
    IContatoRepository contatoRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AdicionarContatosCommand, Result<List<ContatoResponse>, Error>>
{
    private readonly IContatoRepository _contatoRepository = contatoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<ContatoResponse>, Error>> Handle(
        AdicionarContatosCommand request,
        CancellationToken cancellationToken)
    {
        List<Contato> contatos = [];

        var resultadoValidacoes = new List<ValidationResult>();

        foreach (var contato in request.Contatos)
        {
            if (!Validator.TryValidateObject(contato,
                                             new ValidationContext(contato),
                                             resultadoValidacoes,
                                             validateAllProperties: true))
            {
                return new Error(
                    "AdicionarContatos.Validacoes",
                    string.Join("\n ", resultadoValidacoes.Select(v => v.ErrorMessage).ToArray()));
            }

            var contatoTelefoneExistente = _contatoRepository
                .ContatoExistenteComMesmoTelefone(contato.Telefone);

            if (contatoTelefoneExistente is not null)
            {
                return new Error(
                    "AdicionarContatos.ContatoExistenteComMesmoTelefone",
                    "Já existe um contato cadastro com telefone informado");
            }

            var contatoEmailExistente = _contatoRepository
                .ContatoExistenteComMesmoEmail(contato.Email);

            if (contatoEmailExistente is not null)
            {
                return new Error(
                    "AdicionarContatos.ContatoExistenteComMesmoEmail",
                    "Já existe um contato cadastro com e-mail informado");
            }

            var novoContato = Contato.CriarContato(
                nome: contato.Nome,
                telefone: contato.Telefone,
                email: contato.Email,
                ddd: contato.DDD);

            if (contatos.Any(c => c.Equals(novoContato)))
            {
                return new Error(
                    "AdicionarContatos.ContatoRepetido",
                    "Não é permitido adicionar contatos repetidos");
            }

            contatos.Add(novoContato);
        }

        _contatoRepository.Adicionar(contatos);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = contatos.Adapt<List<ContatoResponse>>();

        return response;
    }
}
