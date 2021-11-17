using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class CarpetaArranqueEvidencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Evidencia",
                table: "ItemsCarpetaArranque",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Evidencia",
                table: "ItemsCarpetaArranque");
        }
    }
}
