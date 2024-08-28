using Agenda.Modules.Contatos.Domain.Abstractions;
using System.Linq.Expressions;

namespace Agenda.Modules.Contatos.Persistence.Repositories;

public class BaseRepository<TEntity>(ContatosDbContext dbContext)
    : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ContatosDbContext _dbContext = dbContext;

    public TEntity? Obter(Expression<Func<TEntity, bool>> filtro)
    {
        return _dbContext.Set<TEntity>().SingleOrDefault(filtro);
    }

    public List<TEntity> ObterTodos()
    {
        List<TEntity> ret = _dbContext.Set<TEntity>().ToList<TEntity>();

        if (ret == null) ret = new List<TEntity>();

        return ret;
    }

    public void Adicionar(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    public void Adicionar(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().AddRange(entities);
    }

    public void Alterar(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public void Excluir(TEntity entity)
    {
        _dbContext.Remove(entity);
    }

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
