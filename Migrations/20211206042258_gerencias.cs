using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class gerencias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GerenciaId",
                table: "Contratos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Gerencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerencias", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Gerencias_GerenciaId",
                table: "Contratos");

            migrationBuilder.DropTable(
                name: "Gerencias");

            migrationBuilder.DropIndex(
                name: "IX_Contratos_GerenciaId",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "GerenciaId",
                table: "Contratos");
        }
    }
}
