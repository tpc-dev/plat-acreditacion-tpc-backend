using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatAcreditacionTPCBackend.Migrations
{
    public partial class initial : Migration
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

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Choferes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choferes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosClasificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosClasificacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosAcreditacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosAcreditacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosCivil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosCivil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EtapasCreacionContrato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapasCreacionContrato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gerencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemsCarpetaArranque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evidencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obligatorio = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCarpetaArranque", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jornadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraInicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraTermino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jornadas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NivelesEducacional",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelesEducacional", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProtocolosIngresos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProtocolosIngresos", x => x.Id);
                });

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
                name: "RegistrosInduccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRealizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosInduccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoVehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVehiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    EtapaCreacionContratoId = table.Column<int>(type: "int", nullable: false),
                    InicioContrato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminoContrato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InicioAcreditacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminoAcreditacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_Contratos_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contratos_EtapasCreacionContrato_EtapaCreacionContratoId",
                        column: x => x.EtapaCreacionContratoId,
                        principalTable: "EtapasCreacionContrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiposDocumentosAcreditacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obligatorio = table.Column<bool>(type: "bit", nullable: false),
                    ItemCarpetaArranqueId = table.Column<int>(type: "int", nullable: false),
                    DocumentoClasificacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumentosAcreditacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiposDocumentosAcreditacion_DocumentosClasificacion_DocumentoClasificacionId",
                        column: x => x.DocumentoClasificacionId,
                        principalTable: "DocumentosClasificacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TiposDocumentosAcreditacion_ItemsCarpetaArranque_ItemCarpetaArranqueId",
                        column: x => x.ItemCarpetaArranqueId,
                        principalTable: "ItemsCarpetaArranque",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trabajadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneroId = table.Column<int>(type: "int", nullable: false),
                    EstadoCivilId = table.Column<int>(type: "int", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NivelEducacionalId = table.Column<int>(type: "int", nullable: false),
                    PaisId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trabajadores_EstadosCivil_EstadoCivilId",
                        column: x => x.EstadoCivilId,
                        principalTable: "EstadosCivil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabajadores_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabajadores_NivelesEducacional_NivelEducacionalId",
                        column: x => x.NivelEducacionalId,
                        principalTable: "NivelesEducacional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabajadores_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrabajadoresTPC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GerenciaId = table.Column<int>(type: "int", nullable: false),
                    GeneroId = table.Column<int>(type: "int", nullable: false),
                    EstadoCivilId = table.Column<int>(type: "int", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NivelEducacionalId = table.Column<int>(type: "int", nullable: false),
                    PaisId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrabajadoresTPC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrabajadoresTPC_EstadosCivil_EstadoCivilId",
                        column: x => x.EstadoCivilId,
                        principalTable: "EstadosCivil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrabajadoresTPC_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrabajadoresTPC_Gerencias_GerenciaId",
                        column: x => x.GerenciaId,
                        principalTable: "Gerencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrabajadoresTPC_NivelesEducacional_NivelEducacionalId",
                        column: x => x.NivelEducacionalId,
                        principalTable: "NivelesEducacional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrabajadoresTPC_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosCovidAccesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperatura = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoVehiculoId = table.Column<int>(type: "int", nullable: false),
                    ChoferId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Choferes_ChoferId",
                        column: x => x.ChoferId,
                        principalTable: "Choferes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehiculos_TipoVehiculos_TipoVehiculoId",
                        column: x => x.TipoVehiculoId,
                        principalTable: "TipoVehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargos_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "EmpresasContratos",
                columns: table => new
                {
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresasContratos", x => new { x.EmpresaId, x.ContratoId });
                    table.ForeignKey(
                        name: "FK_EmpresasContratos_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresasContratos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresasContratos_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaTermino = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiasLaborales = table.Column<int>(type: "int", nullable: false),
                    DiasFestivos = table.Column<int>(type: "int", nullable: false),
                    HorasSemana = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JornadaId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turnos_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turnos_Jornadas_JornadaId",
                        column: x => x.JornadaId,
                        principalTable: "Jornadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContratoTiposDocumentoAcreditacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    TipoDocumentoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaTermino = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoTiposDocumentoAcreditacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContratoTiposDocumentoAcreditacion_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratoTiposDocumentoAcreditacion_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ContratoTiposDocumentoAcreditacion_TiposDocumentosAcreditacion_TipoDocumentoAcreditacionId",
                        column: x => x.TipoDocumentoAcreditacionId,
                        principalTable: "TiposDocumentosAcreditacion",
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
                    EmpresaContratoContratoId = table.Column<int>(type: "int", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: true),
                    EmpresaContratoEmpresaId = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: true),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaTermino = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaTiposDocumentosAcreditacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpresaTiposDocumentosAcreditacion_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmpresaTiposDocumentosAcreditacion_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id");
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
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Apellido2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoRolId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    TrabajadorTPCId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_TipoRoles_TipoRolId",
                        column: x => x.TipoRolId,
                        principalTable: "TipoRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_TrabajadoresTPC_TrabajadorTPCId",
                        column: x => x.TrabajadorTPCId,
                        principalTable: "TrabajadoresTPC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContratosVehiculos",
                columns: table => new
                {
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: false),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratosVehiculos", x => new { x.ContratoId, x.VehiculoId });
                    table.ForeignKey(
                        name: "FK_ContratosVehiculos_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratosVehiculos_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ContratosVehiculos_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistroAccesosVehiculosContrato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaEvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoEvento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContratoVehiculoContratoId = table.Column<int>(type: "int", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: true),
                    ContratoVehiculoVehiculoId = table.Column<int>(type: "int", nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroAccesosVehiculosContrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroAccesosVehiculosContrato_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegistroAccesosVehiculosContrato_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehiculoTiposDocumentosAcreditacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDocumentoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    ContratoVehiculoContratoId = table.Column<int>(type: "int", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: true),
                    ContratoVehiculoVehiculoId = table.Column<int>(type: "int", nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: true),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaTermino = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculoTiposDocumentosAcreditacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiculoTiposDocumentosAcreditacion_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehiculoTiposDocumentosAcreditacion_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehiculoTiposDocumentosAcreditacion_TiposDocumentosAcreditacion_TipoDocumentoAcreditacionId",
                        column: x => x.TipoDocumentoAcreditacionId,
                        principalTable: "TiposDocumentosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehiculoTiposDocumentosAcreditacion_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "ContratosTrabajadores",
                columns: table => new
                {
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    TrabajadorId = table.Column<int>(type: "int", nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratosTrabajadores", x => new { x.ContratoId, x.TrabajadorId });
                    table.ForeignKey(
                        name: "FK_ContratosTrabajadores_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratosTrabajadores_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ContratosTrabajadores_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ContratosTrabajadores_Trabajadores_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "Trabajadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratosTrabajadores_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "HistoricosAcreditacionContratoTipoDocumentoAcreditacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratoTipoDocumentoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosAcreditacionContratoTipoDocumentoAcreditacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosAcreditacionContratoTipoDocumentoAcreditacion_ContratoTiposDocumentoAcreditacion_ContratoTipoDocumentoAcreditacion~",
                        column: x => x.ContratoTipoDocumentoAcreditacionId,
                        principalTable: "ContratoTiposDocumentoAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricosAcreditacionContratoTipoDocumentoAcreditacion_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    EmpresaTipoDocumentoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion_EmpresaTiposDocumentosAcreditacion_EmpresaTipoDocumentoAcreditacionId",
                        column: x => x.EmpresaTipoDocumentoAcreditacionId,
                        principalTable: "EmpresaTiposDocumentosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ContratosUsuarios",
                columns: table => new
                {
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratosUsuarios", x => new { x.UsuarioId, x.ContratoId });
                    table.ForeignKey(
                        name: "FK_ContratosUsuarios_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratosUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NombradasDiaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraInicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraTermino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NombradasDiaria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NombradasDiaria_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrabajadoresFrecuente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrabajadoresFrecuente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrabajadoresFrecuente_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    Hora = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehiculoTipoDocumentoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion_VehiculoTiposDocumentosAcreditacion_VehiculoTipoDocumentoAcreditacio~",
                        column: x => x.VehiculoTipoDocumentoAcreditacionId,
                        principalTable: "VehiculoTiposDocumentosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RegistroAccesosTrabajadoresContrato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaEvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoEvento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContratoTrabajadorContratoId = table.Column<int>(type: "int", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: true),
                    ContratoTrabajadorTrabajadorId = table.Column<int>(type: "int", nullable: false),
                    TrabajadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroAccesosTrabajadoresContrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroAccesosTrabajadoresContrato_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegistroAccesosTrabajadoresContrato_ContratosTrabajadores_ContratoTrabajadorContratoId_ContratoTrabajadorTrabajadorId",
                        columns: x => new { x.ContratoTrabajadorContratoId, x.ContratoTrabajadorTrabajadorId },
                        principalTable: "ContratosTrabajadores",
                        principalColumns: new[] { "ContratoId", "TrabajadorId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroAccesosTrabajadoresContrato_Trabajadores_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "Trabajadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrabajadorTiposDocumentoAcreditacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDocumentoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    ContratoTrabajadorContratoId = table.Column<int>(type: "int", nullable: false),
                    ContratoId = table.Column<int>(type: "int", nullable: true),
                    ContratoTrabajadorTrabajadorId = table.Column<int>(type: "int", nullable: false),
                    TrabajadorId = table.Column<int>(type: "int", nullable: true),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaTermino = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrabajadorTiposDocumentoAcreditacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrabajadorTiposDocumentoAcreditacion_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrabajadorTiposDocumentoAcreditacion_ContratosTrabajadores_ContratoTrabajadorContratoId_ContratoTrabajadorTrabajadorId",
                        columns: x => new { x.ContratoTrabajadorContratoId, x.ContratoTrabajadorTrabajadorId },
                        principalTable: "ContratosTrabajadores",
                        principalColumns: new[] { "ContratoId", "TrabajadorId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrabajadorTiposDocumentoAcreditacion_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TrabajadorTiposDocumentoAcreditacion_TiposDocumentosAcreditacion_TipoDocumentoAcreditacionId",
                        column: x => x.TipoDocumentoAcreditacionId,
                        principalTable: "TiposDocumentosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrabajadorTiposDocumentoAcreditacion_Trabajadores_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "Trabajadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NombradasDiariasTrabajadoresFrecuente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombradaDiariaId = table.Column<int>(type: "int", nullable: false),
                    TrabajadorFrecuenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NombradasDiariasTrabajadoresFrecuente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NombradasDiariasTrabajadoresFrecuente_NombradasDiaria_NombradaDiariaId",
                        column: x => x.NombradaDiariaId,
                        principalTable: "NombradasDiaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NombradasDiariasTrabajadoresFrecuente_TrabajadoresFrecuente_TrabajadorFrecuenteId",
                        column: x => x.TrabajadorFrecuenteId,
                        principalTable: "TrabajadoresFrecuente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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

            migrationBuilder.CreateTable(
                name: "HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabajadorTipoDocumentoAcreditacionId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoAcreditacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion_EstadosAcreditacion_EstadoAcreditacionId",
                        column: x => x.EstadoAcreditacionId,
                        principalTable: "EstadosAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion_TrabajadorTiposDocumentoAcreditacion_TrabajadorTipoDocumentoAcredi~",
                        column: x => x.TrabajadorTipoDocumentoAcreditacionId,
                        principalTable: "TrabajadorTiposDocumentoAcreditacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_ContratoId",
                table: "Cargos",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarpetasArranques_ContratoId",
                table: "CarpetasArranques",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_AreaId",
                table: "Contratos",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_EstadoAcreditacionId",
                table: "Contratos",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_EtapaCreacionContratoId",
                table: "Contratos",
                column: "EtapaCreacionContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratosTrabajadores_CargoId",
                table: "ContratosTrabajadores",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratosTrabajadores_EstadoAcreditacionId",
                table: "ContratosTrabajadores",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratosTrabajadores_TrabajadorId",
                table: "ContratosTrabajadores",
                column: "TrabajadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratosTrabajadores_TurnoId",
                table: "ContratosTrabajadores",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratosUsuarios_ContratoId",
                table: "ContratosUsuarios",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratosVehiculos_EstadoAcreditacionId",
                table: "ContratosVehiculos",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratosVehiculos_VehiculoId",
                table: "ContratosVehiculos",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoTiposDocumentoAcreditacion_ContratoId",
                table: "ContratoTiposDocumentoAcreditacion",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoTiposDocumentoAcreditacion_EstadoAcreditacionId",
                table: "ContratoTiposDocumentoAcreditacion",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoTiposDocumentoAcreditacion_TipoDocumentoAcreditacionId",
                table: "ContratoTiposDocumentoAcreditacion",
                column: "TipoDocumentoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresasContratos_ContratoId",
                table: "EmpresasContratos",
                column: "ContratoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpresasContratos_EstadoAcreditacionId",
                table: "EmpresasContratos",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaTiposDocumentosAcreditacion_ContratoId",
                table: "EmpresaTiposDocumentosAcreditacion",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaTiposDocumentosAcreditacion_EmpresaId",
                table: "EmpresaTiposDocumentosAcreditacion",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaTiposDocumentosAcreditacion_EstadoAcreditacionId",
                table: "EmpresaTiposDocumentosAcreditacion",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaTiposDocumentosAcreditacion_TipoDocumentoAcreditacionId",
                table: "EmpresaTiposDocumentosAcreditacion",
                column: "TipoDocumentoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAcreditacionContratoTipoDocumentoAcreditacion_ContratoTipoDocumentoAcreditacionId",
                table: "HistoricosAcreditacionContratoTipoDocumentoAcreditacion",
                column: "ContratoTipoDocumentoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAcreditacionContratoTipoDocumentoAcreditacion_EstadoAcreditacionId",
                table: "HistoricosAcreditacionContratoTipoDocumentoAcreditacion",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion_EmpresaTipoDocumentoAcreditacionId",
                table: "HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion",
                column: "EmpresaTipoDocumentoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion_EstadoAcreditacionId",
                table: "HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion_EstadoAcreditacionId",
                table: "HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion_TrabajadorTipoDocumentoAcreditacionId",
                table: "HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion",
                column: "TrabajadorTipoDocumentoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion_EstadoAcreditacionId",
                table: "HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion_VehiculoTipoDocumentoAcreditacionId",
                table: "HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion",
                column: "VehiculoTipoDocumentoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_IngresosVisitas_VisitaId",
                table: "IngresosVisitas",
                column: "VisitaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCarpetasArranqueCarpetasArranque_CarpetaArranqueId",
                table: "ItemsCarpetasArranqueCarpetasArranque",
                column: "CarpetaArranqueId");

            migrationBuilder.CreateIndex(
                name: "IX_NombradasDiaria_UsuarioId",
                table: "NombradasDiaria",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_NombradasDiariasTrabajadoresFrecuente_NombradaDiariaId",
                table: "NombradasDiariasTrabajadoresFrecuente",
                column: "NombradaDiariaId");

            migrationBuilder.CreateIndex(
                name: "IX_NombradasDiariasTrabajadoresFrecuente_TrabajadorFrecuenteId",
                table: "NombradasDiariasTrabajadoresFrecuente",
                column: "TrabajadorFrecuenteId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroAccesosTrabajadoresContrato_ContratoId",
                table: "RegistroAccesosTrabajadoresContrato",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroAccesosTrabajadoresContrato_ContratoTrabajadorContratoId_ContratoTrabajadorTrabajadorId",
                table: "RegistroAccesosTrabajadoresContrato",
                columns: new[] { "ContratoTrabajadorContratoId", "ContratoTrabajadorTrabajadorId" });

            migrationBuilder.CreateIndex(
                name: "IX_RegistroAccesosTrabajadoresContrato_TrabajadorId",
                table: "RegistroAccesosTrabajadoresContrato",
                column: "TrabajadorId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroAccesosVehiculosContrato_ContratoId",
                table: "RegistroAccesosVehiculosContrato",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroAccesosVehiculosContrato_VehiculoId",
                table: "RegistroAccesosVehiculosContrato",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosCovidAccesos_RegistroCovidFormularioId",
                table: "RegistrosCovidAccesos",
                column: "RegistroCovidFormularioId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposDocumentosAcreditacion_DocumentoClasificacionId",
                table: "TiposDocumentosAcreditacion",
                column: "DocumentoClasificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposDocumentosAcreditacion_ItemCarpetaArranqueId",
                table: "TiposDocumentosAcreditacion",
                column: "ItemCarpetaArranqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajadores_EstadoCivilId",
                table: "Trabajadores",
                column: "EstadoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajadores_GeneroId",
                table: "Trabajadores",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajadores_NivelEducacionalId",
                table: "Trabajadores",
                column: "NivelEducacionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajadores_PaisId",
                table: "Trabajadores",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadoresFrecuente_UsuarioId",
                table: "TrabajadoresFrecuente",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadoresTPC_EstadoCivilId",
                table: "TrabajadoresTPC",
                column: "EstadoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadoresTPC_GeneroId",
                table: "TrabajadoresTPC",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadoresTPC_GerenciaId",
                table: "TrabajadoresTPC",
                column: "GerenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadoresTPC_NivelEducacionalId",
                table: "TrabajadoresTPC",
                column: "NivelEducacionalId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadoresTPC_PaisId",
                table: "TrabajadoresTPC",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorTiposDocumentoAcreditacion_ContratoId",
                table: "TrabajadorTiposDocumentoAcreditacion",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorTiposDocumentoAcreditacion_ContratoTrabajadorContratoId_ContratoTrabajadorTrabajadorId",
                table: "TrabajadorTiposDocumentoAcreditacion",
                columns: new[] { "ContratoTrabajadorContratoId", "ContratoTrabajadorTrabajadorId" });

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorTiposDocumentoAcreditacion_EstadoAcreditacionId",
                table: "TrabajadorTiposDocumentoAcreditacion",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorTiposDocumentoAcreditacion_TipoDocumentoAcreditacionId",
                table: "TrabajadorTiposDocumentoAcreditacion",
                column: "TipoDocumentoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorTiposDocumentoAcreditacion_TrabajadorId",
                table: "TrabajadorTiposDocumentoAcreditacion",
                column: "TrabajadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_ContratoId",
                table: "Turnos",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_JornadaId",
                table: "Turnos",
                column: "JornadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpresaId",
                table: "Usuarios",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TipoRolId",
                table: "Usuarios",
                column: "TipoRolId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TrabajadorTPCId",
                table: "Usuarios",
                column: "TrabajadorTPCId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_ChoferId",
                table: "Vehiculos",
                column: "ChoferId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_TipoVehiculoId",
                table: "Vehiculos",
                column: "TipoVehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculoTiposDocumentosAcreditacion_ContratoId",
                table: "VehiculoTiposDocumentosAcreditacion",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculoTiposDocumentosAcreditacion_EstadoAcreditacionId",
                table: "VehiculoTiposDocumentosAcreditacion",
                column: "EstadoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculoTiposDocumentosAcreditacion_TipoDocumentoAcreditacionId",
                table: "VehiculoTiposDocumentosAcreditacion",
                column: "TipoDocumentoAcreditacionId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculoTiposDocumentosAcreditacion_VehiculoId",
                table: "VehiculoTiposDocumentosAcreditacion",
                column: "VehiculoId");

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
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ContratosUsuarios");

            migrationBuilder.DropTable(
                name: "ContratosVehiculos");

            migrationBuilder.DropTable(
                name: "EmpresasContratos");

            migrationBuilder.DropTable(
                name: "HistoricosAcreditacionContratoTipoDocumentoAcreditacion");

            migrationBuilder.DropTable(
                name: "HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion");

            migrationBuilder.DropTable(
                name: "HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion");

            migrationBuilder.DropTable(
                name: "HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion");

            migrationBuilder.DropTable(
                name: "IngresosVisitas");

            migrationBuilder.DropTable(
                name: "ItemsCarpetasArranqueCarpetasArranque");

            migrationBuilder.DropTable(
                name: "NombradasDiariasTrabajadoresFrecuente");

            migrationBuilder.DropTable(
                name: "ProtocolosIngresos");

            migrationBuilder.DropTable(
                name: "RegistroAccesosTrabajadoresContrato");

            migrationBuilder.DropTable(
                name: "RegistroAccesosVehiculosContrato");

            migrationBuilder.DropTable(
                name: "RegistrosCovidAccesos");

            migrationBuilder.DropTable(
                name: "RegistrosInduccion");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ContratoTiposDocumentoAcreditacion");

            migrationBuilder.DropTable(
                name: "EmpresaTiposDocumentosAcreditacion");

            migrationBuilder.DropTable(
                name: "TrabajadorTiposDocumentoAcreditacion");

            migrationBuilder.DropTable(
                name: "VehiculoTiposDocumentosAcreditacion");

            migrationBuilder.DropTable(
                name: "Visitas");

            migrationBuilder.DropTable(
                name: "CarpetasArranques");

            migrationBuilder.DropTable(
                name: "NombradasDiaria");

            migrationBuilder.DropTable(
                name: "TrabajadoresFrecuente");

            migrationBuilder.DropTable(
                name: "RegistrosCovidFormularios");

            migrationBuilder.DropTable(
                name: "ContratosTrabajadores");

            migrationBuilder.DropTable(
                name: "TiposDocumentosAcreditacion");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Trabajadores");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "DocumentosClasificacion");

            migrationBuilder.DropTable(
                name: "ItemsCarpetaArranque");

            migrationBuilder.DropTable(
                name: "Choferes");

            migrationBuilder.DropTable(
                name: "TipoVehiculos");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "TipoRoles");

            migrationBuilder.DropTable(
                name: "TrabajadoresTPC");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Jornadas");

            migrationBuilder.DropTable(
                name: "EstadosCivil");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Gerencias");

            migrationBuilder.DropTable(
                name: "NivelesEducacional");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "EstadosAcreditacion");

            migrationBuilder.DropTable(
                name: "EtapasCreacionContrato");
        }
    }
}
