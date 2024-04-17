using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Entities;
using Agenda.FIAP.Api.Domain.Enums;
using Agenda.FIAP.Api.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Agenda.FIAP.Api.Infrastructure.Data.Repositories;

internal sealed class ContatoRepository : BaseRepository<Contato>, IContatoRepository
{
    private readonly DataContext dataContext;

    public ContatoRepository(DataContext dataContext)
       : base(dataContext)
    {
        this.dataContext = dataContext;
    }

    public IEnumerable<Contato> ObterPorFiltro(DDD ddd)
    {
        var consulta = dataContext.Contato.AsNoTracking();

        if(ddd > 0)
            consulta = consulta.Where(x => x.DDD == ddd);

        return consulta;
    }
}
