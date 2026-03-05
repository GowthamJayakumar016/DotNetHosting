using Microsoft.EntityFrameworkCore;
using ReactAndJwt.Model;
using System.Reflection.Emit;
namespace ReactAndJwt.Appdata
{
    public class AppDbContext:DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .Property(a => a.Income)
                .HasPrecision(18, 2);
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
       
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Application>Application{ get; set; }
    }
}
