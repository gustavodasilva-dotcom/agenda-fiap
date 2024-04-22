using Agenda.FIAP.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Repositories.Configurations
{
    public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder) {

            builder.ToTable(nameof(Contato));
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id).HasColumnType("int");
            builder.Property(p => p.Telefone).HasMaxLength(9);
        }
    }
}
