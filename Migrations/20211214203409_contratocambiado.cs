using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class contratocambiado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_ContratoUsuario_ContratoUsuarioId",
                table: "Contratos");

            migrationBuilder.DropTable(
                name: "ContratoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_Contratos_ContratoUsuarioId",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "ContratoUsuarioId",
                table: "Contratos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContratoUsuarioId",
                table: "Contratos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContratoUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoUsuario", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_ContratoUsuarioId",
                table: "Contratos",
                column: "ContratoUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_ContratoUsuario_ContratoUsuarioId",
                table: "Contratos",
                column: "ContratoUsuarioId",
                principalTable: "ContratoUsuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
