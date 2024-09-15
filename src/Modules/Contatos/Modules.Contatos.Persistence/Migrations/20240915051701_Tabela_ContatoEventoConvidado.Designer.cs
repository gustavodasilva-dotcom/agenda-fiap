﻿// <auto-generated />
using Agenda.Modules.Contatos.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Modules.Contatos.Persistence.Migrations
{
    [DbContext(typeof(ContatosDbContext))]
    [Migration("20240915051701_Tabela_ContatoEventoConvidado")]
    partial class Tabela_ContatoEventoConvidado
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("contatos")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Agenda.Modules.Contatos.Domain.ContatoEventoConvidado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContatoId")
                        .HasColumnType("int");

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<int>("StatusAceiteEvento")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContatoId");

                    b.HasIndex("EventoId", "ContatoId")
                        .IsUnique();

                    b.ToTable("ContatoEventosConvidado", "contatos", t =>
                        {
                            t.HasCheckConstraint("CK_ContatoEventosConvidado_StatusAceiteEvento_Enum", "[StatusAceiteEvento] BETWEEN 0 AND 2");
                        });
                });

            modelBuilder.Entity("Agenda.Modules.Contatos.Domain.Entities.Contato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DDD")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.HasKey("Id");

                    b.ToTable("Contatos", "contatos", t =>
                        {
                            t.HasCheckConstraint("CK_Contatos_DDD_Enum", "[DDD] IN (11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 24, 27, 28, 31, 32, 33, 34, 35, 37, 38, 41, 42, 43, 44, 45, 46, 47, 48, 49, 51, 53, 54, 55, 61, 62, 63, 64, 65, 66, 67, 68, 69, 71, 73, 74, 75, 77, 79, 81, 82, 83, 84, 85, 86, 87, 88, 89, 91, 92, 93, 94, 95, 96, 97, 98, 99)");
                        });
                });

            modelBuilder.Entity("Agenda.Modules.Contatos.Domain.ContatoEventoConvidado", b =>
                {
                    b.HasOne("Agenda.Modules.Contatos.Domain.Entities.Contato", null)
                        .WithMany("EventosConvidado")
                        .HasForeignKey("ContatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Agenda.Modules.Contatos.Domain.Entities.Contato", b =>
                {
                    b.Navigation("EventosConvidado");
                });
#pragma warning restore 612, 618
        }
    }
}
