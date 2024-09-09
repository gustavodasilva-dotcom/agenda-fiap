using Agenda.Common.Shared.Abstractions;
using Agenda.Modules.Contatos.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Agenda.Common.Shared.Repositories;

public class BaseRepository<TDbContext, TEntity>(TDbContext dbContext)
    : IBaseRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : BaseEntity
{
    private readonly TDbContext _dbContext = dbContext;

    public virtual TEntity? Obter(Expression<Func<TEntity, bool>> filtro)
        => _dbContext.Set<TEntity>().SingleOrDefault(filtro);

    public virtual List<TEntity> ObterTodos()
    {
        List<TEntity> ret = [.. _dbContext.Set<TEntity>()];

        if (ret == null) ret = new List<TEntity>();

        return ret;
    }

    public void Adicionar(TEntity entity)
        => _dbContext.Set<TEntity>().Add(entity);

    public void Adicionar(IEnumerable<TEntity> entities)
        => _dbContext.Set<TEntity>().AddRange(entities);

    public void Alterar(TEntity entity)
        => _dbContext.Set<TEntity>().Update(entity);

    public void Excluir(TEntity entity)
        => _dbContext.Remove(entity);

    public void Dispose()
    {
        try
        {
            if (_dbContext != null) _dbContext.Dispose();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
