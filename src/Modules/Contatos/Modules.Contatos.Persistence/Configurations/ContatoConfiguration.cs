using Agenda.Modules.Contatos.Domain.Entities;
using Agenda.Modules.Contatos.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Modules.Contatos.Persistence.Configurations;

public sealed partial class ContatoConfiguration : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable(InfrastructureConstants.ContatosTable);

        builder.HasKey(c => c.Id);
        
        builder.Property(p => p.Id)
            .HasColumnType(typeName: InfrastructureConstants.IntColumnType);
        
        builder.Property(p => p.Telefone)
            .HasMaxLength(maxLength: InfrastructureConstants.MaxLength9);

        builder.HasMany(p => p.EventosConvidado)
            .WithOne()
            .HasForeignKey(e => e.ContatoId);
    }
}
