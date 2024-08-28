using Agenda.Modules.Contatos.Domain.Abstractions;

namespace Agenda.Modules.Contatos.Persistence.Repositories;

public sealed class UnitOfWork(ContatosDbContext dbContext) : IUnitOfWork
{
    private readonly ContatosDbContext _dbContext = dbContext;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
