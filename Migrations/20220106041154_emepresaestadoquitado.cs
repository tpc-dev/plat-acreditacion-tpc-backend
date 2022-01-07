using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class emepresaestadoquitado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_EstadosAcreditacion_EstadoAcreditacionId",
                table: "Empresas");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_EstadoAcreditacionId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "EstadoAcreditacionId",
                table: "Empresas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoAcreditacionId",
                table: "Empresas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_EstadoAcreditacionId",
                table: "Empresas",
                column: "EstadoAcreditacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_EstadosAcreditacion_EstadoAcreditacionId",
                table: "Empresas",
                column: "EstadoAcreditacionId",
                principalTable: "EstadosAcreditacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
