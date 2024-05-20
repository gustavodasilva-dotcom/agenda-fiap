using Agenda.Domain.Abstractions;
using Agenda.Infrastructure.Data.Context;

namespace Agenda.Infrastructure.Data.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;

    public UnitOfWork(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
