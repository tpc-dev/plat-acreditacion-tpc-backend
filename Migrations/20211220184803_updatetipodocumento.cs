using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class updatetipodocumento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PerteneA",
                table: "TiposDocumentosAcreditacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PerteneA",
                table: "TiposDocumentosAcreditacion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
