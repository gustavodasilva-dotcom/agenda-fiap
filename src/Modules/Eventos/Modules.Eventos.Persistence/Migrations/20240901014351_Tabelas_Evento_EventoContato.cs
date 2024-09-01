using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Eventos.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Tabelas_Evento_EventoContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "eventos");

            migrationBuilder.CreateTable(
                name: "Eventos",
                schema: "eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DataEvento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventoContatos",
                schema: "eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    ContatoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoContatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventoContatos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalSchema: "eventos",
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventoContatos_EventoId_ContatoId",
                schema: "eventos",
                table: "EventoContatos",
                columns: new[] { "EventoId", "ContatoId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventoContatos",
                schema: "eventos");

            migrationBuilder.DropTable(
                name: "Eventos",
                schema: "eventos");
        }
    }
}
