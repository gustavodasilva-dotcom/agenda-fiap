﻿// <auto-generated />
using System;
using Agenda.Modules.Eventos.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Modules.Eventos.Persistence.Migrations
{
    [DbContext(typeof(EventosDbContext))]
    [Migration("20240908200115_Tabelas_Evento_EventoContato")]
    partial class Tabelas_Evento_EventoContato
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("eventos")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Agenda.Modules.Eventos.Domain.Entities.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataEventoFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataEventoInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Eventos", "eventos");
                });

            modelBuilder.Entity("Agenda.Modules.Eventos.Domain.Entities.EventoContato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContatoId")
                        .HasColumnType("int");

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventoId", "ContatoId")
                        .IsUnique();

                    b.ToTable("EventoContatos", "eventos");
                });

            modelBuilder.Entity("Agenda.Modules.Eventos.Domain.Entities.EventoContato", b =>
                {
                    b.HasOne("Agenda.Modules.Eventos.Domain.Entities.Evento", null)
                        .WithMany("Contatos")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Agenda.Modules.Eventos.Domain.Entities.Evento", b =>
                {
                    b.Navigation("Contatos");
                });
#pragma warning restore 612, 618
        }
    }
}
