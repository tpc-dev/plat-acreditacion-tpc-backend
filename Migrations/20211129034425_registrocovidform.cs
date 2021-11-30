using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class registrocovidform : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistrosCovidFormularios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HaTenidoSintomas = table.Column<bool>(type: "bit", nullable: false),
                    Sintomas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HaTenidoContactoEstrecho = table.Column<bool>(type: "bit", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosCovidFormularios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosCovidAccesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperatura = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    RegistroCovidFormularioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosCovidAccesos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosCovidAccesos_RegistrosCovidFormularios_RegistroCovidFormularioId",
                        column: x => x.RegistroCovidFormularioId,
                        principalTable: "RegistrosCovidFormularios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosCovidAccesos_RegistroCovidFormularioId",
                table: "RegistrosCovidAccesos",
                column: "RegistroCovidFormularioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosCovidAccesos");

            migrationBuilder.DropTable(
                name: "RegistrosCovidFormularios");
        }
    }
}
