using Agenda.FIAP.Api.Domain.Entities;
using MediatR;

namespace Agenda.FIAP.Api.Application.Contatos.Queries.GetContatos;

internal sealed class GetContatosQueryHandler
    : IRequestHandler<GetContatosQuery, IEnumerable<Contato>>
{
    public Task<IEnumerable<Contato>> Handle(
        GetContatosQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Contato> contatos =
        [
            new Contato
            (
                id: 1,
                nome: "João da Silva",
                telefone: "(11) 95454-1111",
                email: "joao.silva@outlook.com"
            ),
            new Contato
            (
                id: 2,
                nome: "Maria das Graças",
                telefone: "(13) 96446-1661",
                email: "maria.dasgracas@gmail.com"
            )
        ];

        return Task.FromResult(contatos);
    }
}
