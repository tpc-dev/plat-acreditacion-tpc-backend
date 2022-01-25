using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend
{
    public class ApplicationDbContextGenetec : IdentityDbContext
    {
        public ApplicationDbContextGenetec(DbContextOptions<ApplicationDbContextGenetec> options) : base(options)
        {
       
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<EventoEnvio>()
            //   .HasKey(e => new
            //   {
            //       e.IdEventoEnvio
            //   });
        }

            
        public DbSet<EventoEnvio> EventosEnvio { get; set; }
    }
}