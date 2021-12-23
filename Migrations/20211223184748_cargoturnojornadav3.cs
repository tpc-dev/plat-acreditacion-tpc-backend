using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class cargoturnojornadav3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContratoId",
                table: "Turnos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_ContratoId",
                table: "Turnos",
                column: "ContratoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Contratos_ContratoId",
                table: "Turnos",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Contratos_ContratoId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_ContratoId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "ContratoId",
                table: "Turnos");
        }
    }
}
