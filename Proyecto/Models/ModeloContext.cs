using Microsoft.EntityFrameworkCore;

namespace ProyectoWeb.Models
{
    public class ModeloContext : DbContext
    {
        public ModeloContext(DbContextOptions<ModeloContext> options)
            : base(options)
        {
        }
        public DbSet<Clientes> cliente { get; set; }
        public DbSet<Usuarios> usuario { get; set; }
        public DbSet<Contactos> contacto { get; set; }
        public DbSet<Reuniones> reunion { get; set; }
        public DbSet<Support> soporte { get; set; }
    }
}