using Agenda.Common.Shared.Abstractions;
using Agenda.Modules.Eventos.Domain.Entities;
using Agenda.Modules.Eventos.Persistence.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Modules.Eventos.Persistence;

public class EventosDbContext(
    DbContextOptions<EventosDbContext> options, IPublisher publisher)
    : BaseDbContext<EventosDbContext>(options, publisher)
{
    public DbSet<Evento> Eventos { get; set; }

    public DbSet<EventoContato> EventoContatos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(
            InfrastructureConstants.Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(
            AssemblyReference.Assembly);
    }
}
