using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class EMpresaContrato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadosAcreditacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosAcreditacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDocumentosAcreditacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumentosAcreditacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresas_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaTiposDocumentosAcreditacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDocumentoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaTiposDocumentosAcreditacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpresaTiposDocumentosAcreditacion_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresaTiposDocumentosAcreditacion_TiposDocumentosAcreditacion_TipoDocumentoAcreditacionId",
                        column: x => x.TipoDocumentoAcreditacionId,
                        principalTable: "TiposDocumentosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoContrato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    InicioContrato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminoContrato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InicioAcreditacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contratos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contratos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_EmpresaId",
                table: "Contratos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_UsuarioId",
                table: "Contratos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_EstadoAcreditacionId",
                table: "Empresas",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaTiposDocumentosAcreditacion_EstadoAcreditacionId",
                table: "EmpresaTiposDocumentosAcreditacion",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaTiposDocumentosAcreditacion_TipoDocumentoAcreditacionId",
                table: "EmpresaTiposDocumentosAcreditacion",
                column: "TipoDocumentoAcreditacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "EmpresaTiposDocumentosAcreditacion");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "TiposDocumentosAcreditacion");

            migrationBuilder.DropTable(
                name: "EstadosAcreditacion");
        }
    }
}
