using Agenda.FIAP.Api.Application.Contracts.Responses;
using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Entities;
using Mapster;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Commands.AdicionarContatos;

internal sealed class AdicionarContatosCommandHandler
    : IRequestHandler<AdicionarContatosCommand, IEnumerable<ContatoResponse>>
{
    private readonly IContatoRepository _contatoRepository;

    public AdicionarContatosCommandHandler(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }

    public Task<IEnumerable<ContatoResponse>> Handle(
        AdicionarContatosCommand request,
        CancellationToken cancellationToken)
    {
        List<Contato> contatos = [];

        foreach (var contato in request.Contatos)
        {
            contatos.Add(
                new Contato(
                    nome: contato.Nome,
                    telefone: contato.Telefone,
                    email: contato.Email,
                    ddd: contato.Ddd));
        }

        _contatoRepository.Adicionar(contatos);

        var response = contatos.Adapt<IEnumerable<ContatoResponse>>();

        return Task.FromResult(response);
    }
}
