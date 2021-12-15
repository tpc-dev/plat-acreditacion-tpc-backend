using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class contratocambiado2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Gerencias_GerenciaId",
                table: "Contratos");

            migrationBuilder.DropIndex(
                name: "IX_Contratos_GerenciaId",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "GerenciaId",
                table: "Contratos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GerenciaId",
                table: "Contratos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_GerenciaId",
                table: "Contratos",
                column: "GerenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Gerencias_GerenciaId",
                table: "Contratos",
                column: "GerenciaId",
                principalTable: "Gerencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
