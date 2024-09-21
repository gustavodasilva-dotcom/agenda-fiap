using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Contatos.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Tabela_ContatoEventoConvidado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContatoEventosConvidado",
                schema: "contatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContatoId = table.Column<int>(type: "int", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    StatusAceiteEvento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContatoEventosConvidado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContatoEventosConvidado_Contatos_ContatoId",
                        column: x => x.ContatoId,
                        principalSchema: "contatos",
                        principalTable: "Contatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContatoEventosConvidado_ContatoId",
                schema: "contatos",
                table: "ContatoEventosConvidado",
                column: "ContatoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContatoEventosConvidado_EventoId_ContatoId",
                schema: "contatos",
                table: "ContatoEventosConvidado",
                columns: new[] { "EventoId", "ContatoId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContatoEventosConvidado",
                schema: "contatos");
        }
    }
}
