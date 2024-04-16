using Agenda.FIAP.Api.Domain.Entities;
using Domain.Enum;

namespace Agenda.FIAP.Api.Domain.Abstractions;

public interface IContatoRepository : IBaseRepository<Contato>
{
    IEnumerable<Contato> ObterPorFiltro(DDD ddd);
}
