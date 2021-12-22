using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class historicoacreditacionempresacontrato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmpresaContrato",
                columns: table => new
                {
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaContrato", x => new { x.EmpresaId, x.ContratoId });
                    table.ForeignKey(
                        name: "FK_EmpresaContrato_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresaContrato_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricosAcreditacionEmpresaContratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaContratoId = table.Column<int>(type: "int", nullable: false),
                    EmpresaContratoEmpresaId = table.Column<int>(type: "int", nullable: false),
                    EmpresaContratoContratoId = table.Column<int>(type: "int", nullable: false),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosAcreditacionEmpresaContratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosAcreditacionEmpresaContratos_EmpresaContrato_EmpresaContratoEmpresaId_EmpresaContratoContratoId",
                        columns: x => new { x.EmpresaContratoEmpresaId, x.EmpresaContratoContratoId },
                        principalTable: "EmpresaContrato",
                        principalColumns: new[] { "EmpresaId", "ContratoId" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HistoricosAcreditacionEmpresaContratos_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaContrato_ContratoId",
                table: "EmpresaContrato",
                column: "ContratoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAcreditacionEmpresaContratos_EmpresaContratoEmpresaId_EmpresaContratoContratoId",
                table: "HistoricosAcreditacionEmpresaContratos",
                columns: new[] { "EmpresaContratoEmpresaId", "EmpresaContratoContratoId" });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAcreditacionEmpresaContratos_EstadoAcreditacionId",
                table: "HistoricosAcreditacionEmpresaContratos",
                column: "EstadoAcreditacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricosAcreditacionEmpresaContratos");

            migrationBuilder.DropTable(
                name: "EmpresaContrato");
        }
    }
}
