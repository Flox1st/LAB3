using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1
{
    public class ApppContext : DbContext
    {
        public DbSet<dog> dogs => Set<dog>();
        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Color> Colors => Set<Color>();
        public DbSet<dogAndColor> dogsAndColors => Set<dogAndColor>();

        public ApppContext(DbContextOptions options) => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }
        public DbSet<WebApplication1.Models.Owner> Owner { get; set; } = default!;

    }
}
