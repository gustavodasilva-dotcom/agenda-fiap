using Agenda.Common.Enums;
using Agenda.Domain.Abstractions;
using Agenda.Domain.Entities;
using Agenda.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.Data.Repositories;

public sealed class ContatoRepository : BaseRepository<Contato>, IContatoRepository
{
    private readonly DataContext dataContext;

    public ContatoRepository(DataContext dataContext)
       : base(dataContext)
    {
        this.dataContext = dataContext;
    }

    public IEnumerable<Contato> ObterPorFiltro(DDDs ddd)
    {
        var consulta = dataContext.Contato.AsNoTracking();

        if (ddd > 0)
            consulta = consulta.Where(x => x.DDD == ddd);

        return consulta;
    }

    public Contato? ContatoExistenteComMesmoTelefone(string telefone)
    {
        return dataContext
            .Contato
            .FirstOrDefault(c => c.Telefone.Equals(telefone.Trim()));
    }

    public Contato? ContatoExistenteComMesmoEmail(string email)
    {
        return dataContext
            .Contato
            .FirstOrDefault(c => c.Email.Equals(email.Trim()));
    }
}
