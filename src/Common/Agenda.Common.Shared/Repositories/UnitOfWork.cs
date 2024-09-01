using Agenda.Common.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Common.Shared.Repositories;

public class UnitOfWork<TDbContext>(TDbContext dbContext)
    : IUnitOfWork where TDbContext : DbContext
{
    private readonly TDbContext _dbContext = dbContext;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        => _dbContext.SaveChangesAsync(cancellationToken);
}
