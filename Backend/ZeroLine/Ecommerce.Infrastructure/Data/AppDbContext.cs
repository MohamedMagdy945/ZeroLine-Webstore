
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ZeroLine.Core.Entities.Product;

namespace ZeroLine.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; } 
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
      
        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
