using Agenda.Common.Shared.Enums;
using Agenda.Common.Shared.Repositories;
using Agenda.Modules.Contatos.Domain.Abstractions;
using Agenda.Modules.Contatos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Modules.Contatos.Persistence.Repositories;

public sealed class ContatoRepository(ContatosDbContext dbContext)
    : BaseRepository<ContatosDbContext, Contato>(dbContext), IContatoRepository
{
    private readonly ContatosDbContext _dbContext = dbContext;

    public IEnumerable<Contato> ObterPorFiltro(DDDs ddd)
    {
        var consulta = _dbContext.Contatos.AsNoTracking();

        if (ddd > 0)
            consulta = consulta.Where(x => x.DDD == ddd);

        return consulta;
    }

    public Contato? ContatoExistenteComMesmoTelefone(string telefone)
    {
        return _dbContext
            .Contatos
            .FirstOrDefault(c => c.Telefone.Equals(telefone.Trim()));
    }

    public Contato? ContatoExistenteComMesmoEmail(string email)
    {
        return _dbContext
            .Contatos
            .FirstOrDefault(c => c.Email.Equals(email.Trim()));
    }
}
