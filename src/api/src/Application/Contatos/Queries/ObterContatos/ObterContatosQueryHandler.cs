using Agenda.FIAP.Api.Application.Contracts.Responses;
using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Entities;
using Mapster;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Queries.ObterContatos;

internal sealed class ObterContatosQueryHandler
    : IRequestHandler<ObterContatosQuery, IEnumerable<ContatoResponse>>
{
    private readonly IContatoRepository contatoRepository;

    public ObterContatosQueryHandler(IContatoRepository contatoRepository)
    {
        this.contatoRepository = contatoRepository;
    }

    public Task<IEnumerable<ContatoResponse>> Handle(
        ObterContatosQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Contato> contatos = contatoRepository.ObterTodos();

        var response = contatos.Adapt<IEnumerable<ContatoResponse>>();

        return Task.FromResult(response);
    }
}
