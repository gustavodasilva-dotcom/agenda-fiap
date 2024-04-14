using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Interface
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity ObterPorId(Expression<Func<TEntity, bool>> filtro);
        List<TEntity> ObterTodos();
        void Adicionar(TEntity entity);
        void Alterar(TEntity entity);
        void Excluir(TEntity entity);
    }
}
