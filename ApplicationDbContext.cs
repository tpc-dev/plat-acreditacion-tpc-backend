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
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // RELACION MUCHO A MUCHOS
        builder.Entity<EmpresaContrato>()
            .HasKey(e => new { e.EmpresaId , e.ContratoId});

        builder.Entity<ContratoUsuario>()
        .HasKey(e => new { e.UsuarioId, e.ContratoId });

        builder.Entity<ItemCarpetaArranqueCarpetaArranque>()
       .HasKey(e => new { e.ItemCarpetaArranqueId, e.CarpetaArranqueId});

    }



    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<Visita> Visitas { get; set; }
    public DbSet<IngresoVisitas> IngresosVisitas { get; set; }
    public DbSet<TipoRol> TipoRoles { get; set; }
    public DbSet<Contrato> Contratos{ get; set; }
    public DbSet<Empresa> Empresas{ get; set; }
    public DbSet<EstadoAcreditacion> EstadosAcreditacion{ get; set; }
    public DbSet<TipoDocumentoAcreditacion> TiposDocumentosAcreditacion{ get; set; }
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
    public DbSet<ContratoUsuario> ContratosUsuarios{ get; set; }
    public DbSet<HistoricoAcreditacionEmpresaContrato> HistoricosAcreditacionEmpresaContratos { get; set; }

}
