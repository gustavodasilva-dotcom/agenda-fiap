using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Infrastructure.Data.Context;

namespace Agenda.FIAP.Api.Infrastructure.Data.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;

    public UnitOfWork(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
