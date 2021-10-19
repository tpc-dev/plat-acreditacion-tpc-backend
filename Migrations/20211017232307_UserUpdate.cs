using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class UserUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_TipoRoles_TipoRolId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Usuarios_EncargadoId",
                table: "Visitas");

            migrationBuilder.DropIndex(
                name: "IX_Visitas_EncargadoId",
                table: "Visitas");

            migrationBuilder.DropColumn(
                name: "EncargadoId",
                table: "Visitas");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Visitas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TipoRolId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_UsuarioId",
                table: "Visitas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TipoRoles_TipoRolId",
                table: "Usuarios",
                column: "TipoRolId",
                principalTable: "TipoRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Usuarios_UsuarioId",
                table: "Visitas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_TipoRoles_TipoRolId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Usuarios_UsuarioId",
                table: "Visitas");

            migrationBuilder.DropIndex(
                name: "IX_Visitas_UsuarioId",
                table: "Visitas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Visitas");

            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "EncargadoId",
                table: "Visitas",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoRolId",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_EncargadoId",
                table: "Visitas",
                column: "EncargadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TipoRoles_TipoRolId",
                table: "Usuarios",
                column: "TipoRolId",
                principalTable: "TipoRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Usuarios_EncargadoId",
                table: "Visitas",
                column: "EncargadoId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
