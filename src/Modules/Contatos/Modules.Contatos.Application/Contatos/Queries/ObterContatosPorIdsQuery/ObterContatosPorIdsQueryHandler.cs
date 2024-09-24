using Agenda.Modules.Contatos.Application.Contracts.Responses;
using Agenda.Modules.Contatos.Domain.Abstractions;
using Agenda.Modules.Contatos.Domain.Entities;
using Mapster;
using MediatR;

namespace Modules.Contatos.Application.Contatos.Queries.ObterContatosPorIdsQuery;

internal class ObterContatosPorIdsQueryHandler
    : IRequestHandler<ObterContatosPorIdsQuery, IEnumerable<ContatoResponse>>
{
    private readonly IContatoRepository _contatoRepository;

    public ObterContatosPorIdsQueryHandler(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }

    public Task<IEnumerable<ContatoResponse>> Handle(
        ObterContatosPorIdsQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Contato> contatos = _contatoRepository.ObterPorFiltro(request.Ids.ToArray());

        var response = contatos.Adapt<IEnumerable<ContatoResponse>>();

        return Task.FromResult(response);
    }

}
