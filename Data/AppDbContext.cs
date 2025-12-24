using Microsoft.EntityFrameworkCore;
using SeminarioCibertec.Models;

namespace SeminarioCibertec.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Seminario> Seminario {  get; set; }
        public DbSet<RegistroAsistencia> RegistroAsistencia { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegistroAsistencia>().HasKey
                (p => new { p.SeminarioId, p.EstudianteId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
