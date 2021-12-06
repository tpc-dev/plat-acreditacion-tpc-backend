using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class empresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
         name: "CreatedAt",
         table: "Empresas",
         nullable: false);

            migrationBuilder.AddColumn<DateTime>(
          name: "UpdatedAt",
          table: "Empresas",
          nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
