using Agenda.Modules.Contatos.Application.Contracts.Responses;
using Agenda.Modules.Contatos.Domain.Abstractions;
using Agenda.Modules.Contatos.Domain.Entities;
using Mapster;
using MediatR;

namespace Agenda.Modules.Contatos.Application.Contatos.Queries.ObterContatos;

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
