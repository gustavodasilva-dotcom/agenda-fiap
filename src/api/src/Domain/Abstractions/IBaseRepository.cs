﻿using System.Linq.Expressions;

namespace Agenda.FIAP.Api.Domain.Abstractions;

public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
{
    TEntity ObterPorId(Expression<Func<TEntity, bool>> filtro);
    
    List<TEntity> ObterTodos();
    
    void Adicionar(TEntity entity);

    void Adicionar(IEnumerable<TEntity> entities);

    void Alterar(TEntity entity);

    void Excluir(TEntity entity);
}