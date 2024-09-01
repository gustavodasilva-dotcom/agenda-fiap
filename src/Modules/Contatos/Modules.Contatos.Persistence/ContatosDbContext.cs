using Agenda.Common.Shared.Abstractions;
using Agenda.Modules.Contatos.Domain.Entities;
using Agenda.Modules.Contatos.Persistence.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Modules.Contatos.Persistence;

public class ContatosDbContext(
    DbContextOptions<ContatosDbContext> options, IPublisher publisher)
    : BaseDbContext<ContatosDbContext>(options, publisher)
{
    public DbSet<Contato> Contatos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(
            InfrastructureConstants.Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(
            AssemblyReference.Assembly);
    }
}
