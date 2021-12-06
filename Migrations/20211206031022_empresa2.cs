using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class empresa2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //migrationBuilder.DropColumn(table: "Empresas", name: "CreatedAt");
            //migrationBuilder.DropColumn(table: "Empresas", name: "UpdatedAt");
            migrationBuilder.AlterColumn<DateTime>(
         name: "CreatedAt",
         table: "Empresas",
         nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
          name: "UpdatedAt",
          table: "Empresas",
          nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
