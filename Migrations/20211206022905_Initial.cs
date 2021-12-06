using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "AspNetRoles",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUsers",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ContratoUsuario",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ContratoUsuario", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DocumentosClasificacion",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Activo = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DocumentosClasificacion", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EstadosAcreditacion",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Activo = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EstadosAcreditacion", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ItemsCarpetaArranque",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Indice = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Evidencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Obligatorio = table.Column<bool>(type: "bit", nullable: false),
            //        Activo = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ItemsCarpetaArranque", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ProtocolosIngresos",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Activo = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProtocolosIngresos", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RegistrosCovidFormularios",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Rut = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Empresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        HaTenidoSintomas = table.Column<bool>(type: "bit", nullable: false),
            //        Sintomas = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        HaTenidoContactoEstrecho = table.Column<bool>(type: "bit", nullable: false),
            //        Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RegistrosCovidFormularios", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TipoRoles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TipoRoles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetRoleClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "AspNetRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserLogins",
            //    columns: table => new
            //    {
            //        LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });       

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserRoles",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "AspNetRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserTokens",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoContrato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContratoUsuarioId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    InicioContrato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminoContrato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InicioAcreditacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminoAcreditacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contratos_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contratos_ContratoUsuario_ContratoUsuarioId",
                        column: x => x.ContratoUsuarioId,
                        principalTable: "ContratoUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "Empresas",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Rut = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false),
            //        Activo = table.Column<bool>(type: "bit", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Empresas", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Empresas_EstadosAcreditacion_EstadoAcreditacionId",
            //            column: x => x.EstadoAcreditacionId,
            //            principalTable: "EstadosAcreditacion",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TiposDocumentosAcreditacion",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PerteneA = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Obligatorio = table.Column<bool>(type: "bit", nullable: false),
            //        ItemCarpetaArranqueId = table.Column<int>(type: "int", nullable: false),
            //        DocumentoClasificacionId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TiposDocumentosAcreditacion", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TiposDocumentosAcreditacion_DocumentosClasificacion_DocumentoClasificacionId",
            //            column: x => x.DocumentoClasificacionId,
            //            principalTable: "DocumentosClasificacion",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_TiposDocumentosAcreditacion_ItemsCarpetaArranque_ItemCarpetaArranqueId",
            //            column: x => x.ItemCarpetaArranqueId,
            //            principalTable: "ItemsCarpetaArranque",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RegistrosCovidAccesos",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Temperatura = table.Column<int>(type: "int", nullable: false),
            //        RegistroCovidFormularioId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RegistrosCovidAccesos", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RegistrosCovidAccesos_RegistrosCovidFormularios_RegistroCovidFormularioId",
            //            column: x => x.RegistroCovidFormularioId,
            //            principalTable: "RegistrosCovidFormularios",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Usuarios",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Apellido1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
            //        Apellido2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
            //        Rut = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TipoRolId = table.Column<int>(type: "int", nullable: false),
            //        Activo = table.Column<bool>(type: "bit", nullable: false),
            //        EmpresaId = table.Column<int>(type: "int", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Usuarios", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Usuarios_Empresas_EmpresaId",
            //            column: x => x.EmpresaId,
            //            principalTable: "Empresas",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Usuarios_TipoRoles_TipoRolId",
            //            column: x => x.TipoRolId,
            //            principalTable: "TipoRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EmpresaTiposDocumentosAcreditacion",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TipoDocumentoAcreditacionId = table.Column<int>(type: "int", nullable: false),
            //        EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EmpresaTiposDocumentosAcreditacion", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_EmpresaTiposDocumentosAcreditacion_EstadosAcreditacion_EstadoAcreditacionId",
            //            column: x => x.EstadoAcreditacionId,
            //            principalTable: "EstadosAcreditacion",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_EmpresaTiposDocumentosAcreditacion_TiposDocumentosAcreditacion_TipoDocumentoAcreditacionId",
            //            column: x => x.TipoDocumentoAcreditacionId,
            //            principalTable: "TiposDocumentosAcreditacion",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "Visitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HaIngresado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    FechaVisita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitas_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngresosVisitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresosVisitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngresosVisitas_Visitas_VisitaId",
                        column: x => x.VisitaId,
                        principalTable: "Visitas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetRoleClaims_RoleId",
            //    table: "AspNetRoleClaims",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "RoleNameIndex",
            //    table: "AspNetRoles",
            //    column: "NormalizedName",
            //    unique: true,
            //    filter: "[NormalizedName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserClaims_UserId",
            //    table: "AspNetUserClaims",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserLogins_UserId",
            //    table: "AspNetUserLogins",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_RoleId",
            //    table: "AspNetUserRoles",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "EmailIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedEmail");

            //migrationBuilder.CreateIndex(
            //    name: "UserNameIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedUserName",
            //    unique: true,
            //    filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_AreaId",
                table: "Contratos",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_ContratoUsuarioId",
                table: "Contratos",
                column: "ContratoUsuarioId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Empresas_EstadoAcreditacionId",
            //    table: "Empresas",
            //    column: "EstadoAcreditacionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EmpresaTiposDocumentosAcreditacion_EstadoAcreditacionId",
            //    table: "EmpresaTiposDocumentosAcreditacion",
            //    column: "EstadoAcreditacionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EmpresaTiposDocumentosAcreditacion_TipoDocumentoAcreditacionId",
            //    table: "EmpresaTiposDocumentosAcreditacion",
            //    column: "TipoDocumentoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_IngresosVisitas_VisitaId",
                table: "IngresosVisitas",
                column: "VisitaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RegistrosCovidAccesos_RegistroCovidFormularioId",
            //    table: "RegistrosCovidAccesos",
            //    column: "RegistroCovidFormularioId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TiposDocumentosAcreditacion_DocumentoClasificacionId",
            //    table: "TiposDocumentosAcreditacion",
            //    column: "DocumentoClasificacionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TiposDocumentosAcreditacion_ItemCarpetaArranqueId",
            //    table: "TiposDocumentosAcreditacion",
            //    column: "ItemCarpetaArranqueId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Usuarios_EmpresaId",
            //    table: "Usuarios",
            //    column: "EmpresaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Usuarios_TipoRolId",
            //    table: "Usuarios",
            //    column: "TipoRolId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_AreaId",
                table: "Visitas",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_UsuarioId",
                table: "Visitas",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "AspNetRoleClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserLogins");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserRoles");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Contratos");

            //migrationBuilder.DropTable(
            //    name: "EmpresaTiposDocumentosAcreditacion");

            migrationBuilder.DropTable(
                name: "IngresosVisitas");

            //migrationBuilder.DropTable(
            //    name: "ProtocolosIngresos");

            //migrationBuilder.DropTable(
            //    name: "RegistrosCovidAccesos");

            //migrationBuilder.DropTable(
            //    name: "AspNetRoles");

            //migrationBuilder.DropTable(
            //    name: "AspNetUsers");

            //migrationBuilder.DropTable(
            //    name: "ContratoUsuario");

            //migrationBuilder.DropTable(
            //    name: "TiposDocumentosAcreditacion");

            migrationBuilder.DropTable(
                name: "Visitas");

            //migrationBuilder.DropTable(
            //    name: "RegistrosCovidFormularios");

            //migrationBuilder.DropTable(
            //    name: "DocumentosClasificacion");

            //migrationBuilder.DropTable(
            //    name: "ItemsCarpetaArranque");

            migrationBuilder.DropTable(
                name: "Areas");

            //migrationBuilder.DropTable(
            //    name: "Usuarios");

            //migrationBuilder.DropTable(
            //    name: "Empresas");

            //migrationBuilder.DropTable(
            //    name: "TipoRoles");

            //migrationBuilder.DropTable(
            //    name: "EstadosAcreditacion");
        }
    }
}
