using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models
{
    public class RegistrarContext : DbContext
    {
        public RegistrarContext (DbContextOptions<RegistrarContext> options)
            : base(options)
        {
        }

        public DbSet<Proyecto.Models.Registrar> Registrar { get; set; }
    }
}