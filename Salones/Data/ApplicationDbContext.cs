using Microsoft.EntityFrameworkCore;
using Salones.Models;

namespace Salones.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Salon> Salones { get; set; }
        public DbSet<EquipoTecnologico> EquiposTecnologicos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
    }
}
