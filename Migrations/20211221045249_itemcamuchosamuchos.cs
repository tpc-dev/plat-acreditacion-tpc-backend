using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class itemcamuchosamuchos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarpetasArranques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarpetasArranques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarpetasArranques_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsCarpetasArranqueCarpetasArranque",
                columns: table => new
                {
                    CarpetaArranqueId = table.Column<int>(type: "int", nullable: false),
                    ItemCarpetaArranqueId = table.Column<int>(type: "int", nullable: false),
                    Obligatorio = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCarpetasArranqueCarpetasArranque", x => new { x.ItemCarpetaArranqueId, x.CarpetaArranqueId });
                    table.ForeignKey(
                        name: "FK_ItemsCarpetasArranqueCarpetasArranque_CarpetasArranques_CarpetaArranqueId",
                        column: x => x.CarpetaArranqueId,
                        principalTable: "CarpetasArranques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsCarpetasArranqueCarpetasArranque_ItemsCarpetaArranque_ItemCarpetaArranqueId",
                        column: x => x.ItemCarpetaArranqueId,
                        principalTable: "ItemsCarpetaArranque",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarpetasArranques_ContratoId",
                table: "CarpetasArranques",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCarpetasArranqueCarpetasArranque_CarpetaArranqueId",
                table: "ItemsCarpetasArranqueCarpetasArranque",
                column: "CarpetaArranqueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsCarpetasArranqueCarpetasArranque");

            migrationBuilder.DropTable(
                name: "CarpetasArranques");
        }
    }
}
