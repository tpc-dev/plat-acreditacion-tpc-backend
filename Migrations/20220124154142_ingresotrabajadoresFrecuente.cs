using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class ingresotrabajadoresFrecuente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistroAccesosTrabajadoresFrecuente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaEvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoEvento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombradaDiariaId = table.Column<int>(type: "int", nullable: false),
                    TrabajadorFrecuenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroAccesosTrabajadoresFrecuente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroAccesosTrabajadoresFrecuente_NombradasDiaria_NombradaDiariaId",
                        column: x => x.NombradaDiariaId,
                        principalTable: "NombradasDiaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroAccesosTrabajadoresFrecuente_TrabajadoresFrecuente_TrabajadorFrecuenteId",
                        column: x => x.TrabajadorFrecuenteId,
                        principalTable: "TrabajadoresFrecuente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistroAccesosTrabajadoresFrecuente_NombradaDiariaId",
                table: "RegistroAccesosTrabajadoresFrecuente",
                column: "NombradaDiariaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroAccesosTrabajadoresFrecuente_TrabajadorFrecuenteId",
                table: "RegistroAccesosTrabajadoresFrecuente",
                column: "TrabajadorFrecuenteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroAccesosTrabajadoresFrecuente");
        }
    }
}
