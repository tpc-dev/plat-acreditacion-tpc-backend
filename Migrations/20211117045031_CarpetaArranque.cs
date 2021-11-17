using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class CarpetaArranque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "TiposDocumentosAcreditacion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ItemCarpetaArranqueId",
                table: "TiposDocumentosAcreditacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "EstadosAcreditacion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "TerminoAcreditacion",
                table: "Contratos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ItemsCarpetaArranque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCarpetaArranque", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TiposDocumentosAcreditacion_ItemCarpetaArranqueId",
                table: "TiposDocumentosAcreditacion",
                column: "ItemCarpetaArranqueId");

            migrationBuilder.AddForeignKey(
                name: "FK_TiposDocumentosAcreditacion_ItemsCarpetaArranque_ItemCarpetaArranqueId",
                table: "TiposDocumentosAcreditacion",
                column: "ItemCarpetaArranqueId",
                principalTable: "ItemsCarpetaArranque",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TiposDocumentosAcreditacion_ItemsCarpetaArranque_ItemCarpetaArranqueId",
                table: "TiposDocumentosAcreditacion");

            migrationBuilder.DropTable(
                name: "ItemsCarpetaArranque");

            migrationBuilder.DropIndex(
                name: "IX_TiposDocumentosAcreditacion_ItemCarpetaArranqueId",
                table: "TiposDocumentosAcreditacion");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "TiposDocumentosAcreditacion");

            migrationBuilder.DropColumn(
                name: "ItemCarpetaArranqueId",
                table: "TiposDocumentosAcreditacion");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "EstadosAcreditacion");

            migrationBuilder.DropColumn(
                name: "TerminoAcreditacion",
                table: "Contratos");
        }
    }
}
