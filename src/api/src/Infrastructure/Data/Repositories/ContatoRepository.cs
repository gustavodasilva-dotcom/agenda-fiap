using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Entities;
using Agenda.FIAP.Api.Infrastructure.Data.Context;

namespace Agenda.FIAP.Api.Infrastructure.Data.Repositories;

internal sealed class ContatoRepository : BaseRepository<Contato>, IContatoRepository
{
    private readonly DataContext dataContext;

    public ContatoRepository(DataContext dataContext)
       : base(dataContext)
    {
        this.dataContext = dataContext;
    }

    public Contato ObterPorId(int id)
    {
        return dataContext.Contato
                          .Where(a => a.Id == id).FirstOrDefault();
    }
}
