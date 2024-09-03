using Agenda.Modules.Eventos.Domain.Entities;
using Agenda.Modules.Eventos.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Modules.Eventos.Persistence.Configurations;

public sealed partial class EventoConfiguration : IEntityTypeConfiguration<Evento>
{
    public void Configure(EntityTypeBuilder<Evento> builder)
    {
        builder.ToTable(InfrastructureConstants.EventosTable);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnType(typeName: InfrastructureConstants.IntColumnType);

        builder.Property(p => p.Nome)
            .HasMaxLength(maxLength: InfrastructureConstants.MaxLength255);

        builder.Property(p => p.DataEventoInicio);

        builder.Property(p => p.DataEventoFinal);

        builder.HasMany(p => p.Contatos)
            .WithOne()
            .HasForeignKey(ec => ec.EventoId);
    }
}
