using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatAcreditacionTPCBackend;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // RELACION MUCHO A MUCHOS
        builder.Entity<EmpresaContrato>()
            .HasKey(e => new { e.EmpresaId, e.ContratoId });

        builder.Entity<ContratoUsuario>()
        .HasKey(e => new { e.UsuarioId, e.ContratoId });

        builder.Entity<ItemCarpetaArranqueCarpetaArranque>()
       .HasKey(e => new { e.ItemCarpetaArranqueId, e.CarpetaArranqueId });

        builder.Entity<ContratoTrabajador>()
       .HasKey(e => new { e.ContratoId, e.TrabajadorId });

        builder.Entity<ContratoVehiculo>()
       .HasKey(e => new { e.ContratoId, e.VehiculoId });
       
    }



    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<Visita> Visitas { get; set; }
    public DbSet<IngresoVisitas> IngresosVisitas { get; set; }
    public DbSet<TipoRol> TipoRoles { get; set; }
    public DbSet<Contrato> Contratos { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<EstadoAcreditacion> EstadosAcreditacion { get; set; }
    public DbSet<TipoDocumentoAcreditacion> TiposDocumentosAcreditacion { get; set; }
    public DbSet<EmpresaTipoDocumentoAcreditacion> EmpresaTiposDocumentosAcreditacion { get; set; }
    public DbSet<ItemCarpetaArranque> ItemsCarpetaArranque { get; set; }
    public DbSet<CarpetaArranque> CarpetasArranques { get; set; }
    public DbSet<ItemCarpetaArranqueCarpetaArranque> ItemsCarpetasArranqueCarpetasArranque { get; set; }
    public DbSet<ProtocoloIngreso> ProtocolosIngresos { get; set; }
    public DbSet<RegistroCovidFormulario> RegistrosCovidFormularios { get; set; }
    public DbSet<RegistroCovidAccesos> RegistrosCovidAccesos { get; set; }
    public DbSet<DocumentoClasificacion> DocumentosClasificacion { get; set; }
    public DbSet<EtapaCreacionContrato> EtapasCreacionContrato { get; set; }
    public DbSet<Gerencia> Gerencias { get; set; }
    public DbSet<EmpresaContrato> EmpresasContratos { get; set; }
    public DbSet<ContratoUsuario> ContratosUsuarios { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<Jornada> Jornadas { get; set; }
    public DbSet<Turno> Turnos { get; set; }
    public DbSet<Trabajador> Trabajadores { get; set; }
    public DbSet<TrabajadorTPC> TrabajadoresTPC { get; set; }
    public DbSet<TrabajadorFrecuente> TrabajadoresFrecuente { get; set; }
    public DbSet<ContratoTrabajador> ContratosTrabajadores { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<EstadoCivil> EstadosCivil { get; set; }
    public DbSet<NivelEducacional> NivelesEducacional { get; set; }
    public DbSet<Pais> Paises { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }
    public DbSet<TipoVehiculo> TipoVehiculos { get; set; }
    public DbSet<ContratoVehiculo> ContratosVehiculos { get; set; }
    public DbSet<Chofer> Choferes { get; set; }
    public DbSet<ContratoTipoDocumentoAcreditacion> ContratoTiposDocumentoAcreditacion { get; set; }
    public DbSet<TrabajadorTipoDocumentoAcreditacion> TrabajadorTiposDocumentoAcreditacion { get; set; }
    public DbSet<VehiculoTipoDocumentoAcreditacion> VehiculoTiposDocumentosAcreditacion { get; set; }
    public DbSet<HistoricoAcreditacionContratoTipoDocumentoAcreditacion> HistoricosAcreditacionContratoTipoDocumentoAcreditacion { get; set; }
    public DbSet<HistoricoAcreditacionTrabajadorTipoDocumentoAcreditacion> HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion { get; set; }
    public DbSet<HistoricoAcreditacionVehiculoTipoDocumentoAcreditacion> HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion { get; set; }
    public DbSet<HistoricoAcreditacionEmpresaTipoDocumentoAcreditacion> HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion { get; set; }
    public DbSet<RegistroInduccion> RegistrosInduccion { get; set; }
    public DbSet<RegistroAccesoTrabajadorContrato> RegistroAccesosTrabajadoresContrato { get; set; }
    public DbSet<RegistroAccesoVehiculoContrato> RegistroAccesosVehiculosContrato { get; set; }
    public DbSet<RegistroAccesoTrabajadorFrecuente> RegistroAccesosTrabajadoresFrecuente { get; set; }
    public DbSet<NombradaDiaria> NombradasDiaria { get; set; }
    public DbSet<NombradaDiariaTrabajadorFrecuente> NombradasDiariasTrabajadoresFrecuente { get; set; }
    public DbSet<CorreoAlertaCovid> CorreosAlertaCovid { get; set; }
}
