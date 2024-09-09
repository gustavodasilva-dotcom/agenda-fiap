using System.Linq.Expressions;
using Agenda.Common.Shared.Abstractions;

namespace Agenda.Modules.Contatos.Domain.Abstractions;

public interface IBaseRepository<TEntity> : IDisposable
    where TEntity : BaseEntity
{
    TEntity? Obter(Expression<Func<TEntity, bool>> filtro);

    List<TEntity> ObterTodos();

    void Adicionar(TEntity entity);

    void Adicionar(IEnumerable<TEntity> entities);

    void Alterar(TEntity entity);

    void Excluir(TEntity entity);
}
