using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class usuariocontrato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContratosUsuarios",
                columns: table => new
                {
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    GerenciaId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratosUsuarios", x => new { x.UsuarioId, x.ContratoId });
                    table.ForeignKey(
                        name: "FK_ContratosUsuarios_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ContratosUsuarios_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ContratosUsuarios_Gerencias_GerenciaId",
                        column: x => x.GerenciaId,
                        principalTable: "Gerencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ContratosUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContratosUsuarios_AreaId",
                table: "ContratosUsuarios",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratosUsuarios_ContratoId",
                table: "ContratosUsuarios",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratosUsuarios_GerenciaId",
                table: "ContratosUsuarios",
                column: "GerenciaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContratosUsuarios");
        }
    }
}
