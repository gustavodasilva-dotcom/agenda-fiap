using Agenda.Application.Contracts.Responses;
using Agenda.Domain.Abstractions;
using Agenda.Domain.Entities;
using Mapster;
using MediatR;

namespace Agenda.Application.Contatos.Queries.ObterContatos;

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
