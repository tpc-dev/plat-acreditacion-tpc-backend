using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class contratocambiado3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EtapaCreacionContratoId",
                table: "Contratos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EtapaCreacionContrato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapaCreacionContrato", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_EtapaCreacionContratoId",
                table: "Contratos",
                column: "EtapaCreacionContratoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_EtapaCreacionContrato_EtapaCreacionContratoId",
                table: "Contratos",
                column: "EtapaCreacionContratoId",
                principalTable: "EtapaCreacionContrato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_EtapaCreacionContrato_EtapaCreacionContratoId",
                table: "Contratos");

            migrationBuilder.DropTable(
                name: "EtapaCreacionContrato");

            migrationBuilder.DropIndex(
                name: "IX_Contratos_EtapaCreacionContratoId",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "EtapaCreacionContratoId",
                table: "Contratos");
        }
    }
}
