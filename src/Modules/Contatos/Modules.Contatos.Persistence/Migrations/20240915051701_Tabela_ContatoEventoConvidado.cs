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
                    table.CheckConstraint("CK_ContatoEventosConvidado_StatusAceiteEvento_Enum", "[StatusAceiteEvento] BETWEEN 0 AND 2");
                    table.ForeignKey(
                        name: "FK_ContatoEventosConvidado_Contatos_ContatoId",
                        column: x => x.ContatoId,
                        principalSchema: "contatos",
                        principalTable: "Contatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Contatos_DDD_Enum",
                schema: "contatos",
                table: "Contatos",
                sql: "[DDD] IN (11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 24, 27, 28, 31, 32, 33, 34, 35, 37, 38, 41, 42, 43, 44, 45, 46, 47, 48, 49, 51, 53, 54, 55, 61, 62, 63, 64, 65, 66, 67, 68, 69, 71, 73, 74, 75, 77, 79, 81, 82, 83, 84, 85, 86, 87, 88, 89, 91, 92, 93, 94, 95, 96, 97, 98, 99)");

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

            migrationBuilder.DropCheckConstraint(
                name: "CK_Contatos_DDD_Enum",
                schema: "contatos",
                table: "Contatos");
        }
    }
}
