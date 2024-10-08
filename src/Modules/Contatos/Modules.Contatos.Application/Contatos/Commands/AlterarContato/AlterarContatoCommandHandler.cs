using Agenda.Common.Shared;
using Agenda.Common.Shared.Abstractions;
using Agenda.Modules.Contatos.Application.Contracts.Responses;
using Agenda.Modules.Contatos.Domain.Abstractions;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Modules.Contatos.Application.Contatos.Commands.AlterarContato;

internal sealed class AlterarContatoCommandHandler(
    IContatoRepository contatoRepository,
    [FromKeyedServices(nameof(Contatos))] IUnitOfWork unitOfWork)
    : IRequestHandler<AlterarContatoCommand, Result<ContatoResponse, Error>>
{
    private readonly IContatoRepository _contatoRepository = contatoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ContatoResponse, Error>> Handle(
        AlterarContatoCommand request,
        CancellationToken cancellationToken)
    {
        var resultadoValidacoes = new List<ValidationResult>();

        if (!Validator.TryValidateObject(request.Contato,
                                         new ValidationContext(request.Contato),
                                         resultadoValidacoes,
                                         validateAllProperties: true))
        {
            return new Error(
                "AlterarContato.Validacoes",
                string.Join("\n ", resultadoValidacoes.Select(v => v.ErrorMessage).ToArray()));
        }

        var contatoTelefoneExistente = _contatoRepository
            .ContatoExistenteComMesmoTelefone(request.Contato.Telefone);

        if (contatoTelefoneExistente is not null &&
            contatoTelefoneExistente.Id != request.Id)
        {
            return new Error(
                "AlterarContato.ContatoExistenteComMesmoTelefone",
                "Já existe um contato cadastro com telefone informado");
        }

        var contatoEmailExistente = _contatoRepository
            .ContatoExistenteComMesmoEmail(request.Contato.Email);

        if (contatoEmailExistente is not null &&
            contatoEmailExistente.Id != request.Id)
        {
            return new Error(
                "AlterarContato.ContatoExistenteComMesmoEmail",
                "Já existe um contato cadastro com e-mail informado");
        }

        var contatoAtualizar = _contatoRepository.Obter(c => c.Id == request.Id);

        if (contatoAtualizar is null)
        {
            return new Error(
                "AlterarContato.ContatoNaoEncontrado",
                "Não foi encontrado nenhum contato com o id informado");
        }

        contatoAtualizar.AtualizarContato(
            nome: request.Contato.Nome,
            telefone: request.Contato.Telefone,
            email: request.Contato.Email,
            ddd: request.Contato.DDD);

        _contatoRepository.Alterar(contatoAtualizar);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = contatoAtualizar.Adapt<ContatoResponse>();

        return response;
    }
}
