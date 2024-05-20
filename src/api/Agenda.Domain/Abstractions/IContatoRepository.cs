using Agenda.Common.Enums;
using Agenda.Domain.Entities;

namespace Agenda.Domain.Abstractions;

public interface IContatoRepository : IBaseRepository<Contato>
{
    IEnumerable<Contato> ObterPorFiltro(DDDs ddd);

    Contato? ContatoExistenteComMesmoTelefone(string telefone);

    Contato? ContatoExistenteComMesmoEmail(string email);
}
