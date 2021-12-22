using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class historicoacreditacionempresacontrato2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpresaContrato_Contratos_ContratoId",
                table: "EmpresaContrato");

            migrationBuilder.DropForeignKey(
                name: "FK_EmpresaContrato_Empresas_EmpresaId",
                table: "EmpresaContrato");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosAcreditacionEmpresaContratos_EmpresaContrato_EmpresaContratoEmpresaId_EmpresaContratoContratoId",
                table: "HistoricosAcreditacionEmpresaContratos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmpresaContrato",
                table: "EmpresaContrato");

            migrationBuilder.RenameTable(
                name: "EmpresaContrato",
                newName: "EmpresasContratos");

            migrationBuilder.RenameIndex(
                name: "IX_EmpresaContrato_ContratoId",
                table: "EmpresasContratos",
                newName: "IX_EmpresasContratos_ContratoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmpresasContratos",
                table: "EmpresasContratos",
                columns: new[] { "EmpresaId", "ContratoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmpresasContratos_Contratos_ContratoId",
                table: "EmpresasContratos",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmpresasContratos_Empresas_EmpresaId",
                table: "EmpresasContratos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosAcreditacionEmpresaContratos_EmpresasContratos_EmpresaContratoEmpresaId_EmpresaContratoContratoId",
                table: "HistoricosAcreditacionEmpresaContratos",
                columns: new[] { "EmpresaContratoEmpresaId", "EmpresaContratoContratoId" },
                principalTable: "EmpresasContratos",
                principalColumns: new[] { "EmpresaId", "ContratoId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpresasContratos_Contratos_ContratoId",
                table: "EmpresasContratos");

            migrationBuilder.DropForeignKey(
                name: "FK_EmpresasContratos_Empresas_EmpresaId",
                table: "EmpresasContratos");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosAcreditacionEmpresaContratos_EmpresasContratos_EmpresaContratoEmpresaId_EmpresaContratoContratoId",
                table: "HistoricosAcreditacionEmpresaContratos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmpresasContratos",
                table: "EmpresasContratos");

            migrationBuilder.RenameTable(
                name: "EmpresasContratos",
                newName: "EmpresaContrato");

            migrationBuilder.RenameIndex(
                name: "IX_EmpresasContratos_ContratoId",
                table: "EmpresaContrato",
                newName: "IX_EmpresaContrato_ContratoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmpresaContrato",
                table: "EmpresaContrato",
                columns: new[] { "EmpresaId", "ContratoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmpresaContrato_Contratos_ContratoId",
                table: "EmpresaContrato",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmpresaContrato_Empresas_EmpresaId",
                table: "EmpresaContrato",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosAcreditacionEmpresaContratos_EmpresaContrato_EmpresaContratoEmpresaId_EmpresaContratoContratoId",
                table: "HistoricosAcreditacionEmpresaContratos",
                columns: new[] { "EmpresaContratoEmpresaId", "EmpresaContratoContratoId" },
                principalTable: "EmpresaContrato",
                principalColumns: new[] { "EmpresaId", "ContratoId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
