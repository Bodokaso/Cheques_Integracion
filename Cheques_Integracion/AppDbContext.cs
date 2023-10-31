using Cheques_Integracion.Models;
using Microsoft.EntityFrameworkCore;

namespace Cheques_Integracion
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ConceptosPago> ConceptosPagos { get; set; }
        public DbSet<Proveedore> Proveedores { get; set; }
        public DbSet<RegistroSolicitudCheque> RegistroSolicitudCheques { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
