using Agenda.Modules.Contatos.Domain.Entities;
using Agenda.Modules.Contatos.Persistence.Constants;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Modules.Contatos.Persistence;

public class ContatosDbContext(
    DbContextOptions<ContatosDbContext> options) : DbContext(options)
{
    public DbSet<Contato> Contatos {get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(InfrastructureConstants.Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ContatosDbContext).Assembly);
    }
}
