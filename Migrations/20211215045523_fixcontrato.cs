using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class fixcontrato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_EtapaCreacionContrato_EtapaCreacionContratoId",
                table: "Contratos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EtapaCreacionContrato",
                table: "EtapaCreacionContrato");

            migrationBuilder.RenameTable(
                name: "EtapaCreacionContrato",
                newName: "EtapasCreacionContrato");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EtapasCreacionContrato",
                table: "EtapasCreacionContrato",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_EtapasCreacionContrato_EtapaCreacionContratoId",
                table: "Contratos",
                column: "EtapaCreacionContratoId",
                principalTable: "EtapasCreacionContrato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_EtapasCreacionContrato_EtapaCreacionContratoId",
                table: "Contratos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EtapasCreacionContrato",
                table: "EtapasCreacionContrato");

            migrationBuilder.RenameTable(
                name: "EtapasCreacionContrato",
                newName: "EtapaCreacionContrato");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EtapaCreacionContrato",
                table: "EtapaCreacionContrato",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_EtapaCreacionContrato_EtapaCreacionContratoId",
                table: "Contratos",
                column: "EtapaCreacionContratoId",
                principalTable: "EtapaCreacionContrato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
