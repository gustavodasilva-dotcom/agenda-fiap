using Agenda.FIAP.Api.Application.Contracts.Responses;
using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Entities;
using Mapster;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Queries.ObterContatos;

internal sealed class ObterContatosQueryHandler
    : IRequestHandler<ObterContatosQuery, IEnumerable<ContatoResponse>>
{
    private readonly IContatoRepository _contatoRepository;

    public ObterContatosQueryHandler(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }

    public Task<IEnumerable<ContatoResponse>> Handle(
        ObterContatosQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Contato> contatos = _contatoRepository.ObterPorFiltro(request.DDD);

        var response = contatos.Adapt<IEnumerable<ContatoResponse>>();

        return Task.FromResult(response);
    }
}
