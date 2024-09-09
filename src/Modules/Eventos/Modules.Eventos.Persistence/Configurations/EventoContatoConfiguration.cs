using Agenda.Modules.Eventos.Domain.Entities;
using Agenda.Modules.Eventos.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Modules.Eventos.Persistence.Configurations;

public sealed partial class EventoContatoConfiguration
    : IEntityTypeConfiguration<EventoContato>
{
    public void Configure(EntityTypeBuilder<EventoContato> builder)
    {
        builder.ToTable(InfrastructureConstants.EventoContatosTable);

        builder.HasKey(p => p.Id);

        builder.HasIndex(p => new
        {
            p.EventoId,
            p.ContatoId
        }).IsUnique();
    }
}
