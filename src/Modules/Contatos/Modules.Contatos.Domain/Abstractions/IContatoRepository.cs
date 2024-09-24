using Agenda.Common.Shared.Enums;
using Agenda.Modules.Contatos.Domain.Entities;

namespace Agenda.Modules.Contatos.Domain.Abstractions;

public interface IContatoRepository : IBaseRepository<Contato>
{
    IEnumerable<Contato> ObterPorFiltro(DDDs ddd);

    IEnumerable<Contato> ObterPorFiltro(int[] ids);

    Contato? ContatoExistenteComMesmoTelefone(string telefone);

    Contato? ContatoExistenteComMesmoEmail(string email);
}
