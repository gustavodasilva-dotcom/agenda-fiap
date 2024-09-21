using Agenda.Modules.Contatos.Domain;
using Agenda.Modules.Contatos.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Modules.Contatos.Persistence.Configurations;

internal sealed partial class ContatoEventoConvidadoConfiguration
    : IEntityTypeConfiguration<ContatoEventoConvidado>
{
    public void Configure(EntityTypeBuilder<ContatoEventoConvidado> builder)
    {
        builder.ToTable(InfrastructureConstants.ContatoEventosConvidadoTable);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnType(typeName: InfrastructureConstants.IntColumnType);

        builder.HasIndex(p => new
        {
            p.EventoId,
            p.ContatoId
        }).IsUnique();
    }
}
