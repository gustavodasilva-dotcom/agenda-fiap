using Agenda.FIAP.Api.Application.Contracts.Responses;
using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Entities;
using Mapster;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Commands.AlterarContato;

internal sealed class AlterarContatoCommandHandler
    : IRequestHandler<AlterarContatoCommand, ContatoResponse>
{
    private readonly IContatoRepository _contatoRepository;

    public AlterarContatoCommandHandler(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }

    public Task<ContatoResponse> Handle(
        AlterarContatoCommand request,
        CancellationToken cancellationToken)
    {
        var contato = _contatoRepository.Obter(c => c.Id == request.Id);

        if (contato is null)
        {
            // TODO: criar Middleware e Exceptions customizadas ou criar retorno
            // com operador implícito para retornar erros.
            throw new Exception("Contato não encontrado");
        }

        var contatoAtualizado = Contato.AtualizarContato(
            id: contato.Id,
            nome: request.Nome,
            telefone: request.Telefone,
            email: request.Email,
            ddd: request.DDD);

        _contatoRepository.Alterar(contatoAtualizado);

        var response = contatoAtualizado.Adapt<ContatoResponse>();

        return Task.FromResult(response);
    }
}
