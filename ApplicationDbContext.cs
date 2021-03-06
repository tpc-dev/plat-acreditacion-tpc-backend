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

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Visita> Visitas { get; set; }
    public DbSet<IngresoVisitas> IngresosVisitas { get; set; }
    public DbSet<TipoRol> TipoRoles { get; set; }
    public DbSet<Contrato> Contratos{ get; set; }
    public DbSet<Empresa> Empresas{ get; set; }
    public DbSet<EstadoAcreditacion> EstadosAcreditacion{ get; set; }
    public DbSet<TipoDocumentoAcreditacion> TiposDocumentosAcreditacion{ get; set; }
    public DbSet<EmpresaTipoDocumentoAcreditacion> EmpresaTiposDocumentosAcreditacion { get; set; }
    public DbSet<ItemCarpetaArranque> ItemsCarpetaArranque { get; set; }


}
