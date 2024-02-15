using Microsoft.EntityFrameworkCore;

namespace products
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsContext(DbContextOptions<ProductsContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(b => b.Price).HasPrecision(10,2).IsRequired();
            modelBuilder.Entity<Product>().Property(b => b.Name).HasMaxLength(64).IsRequired();
            modelBuilder.Entity<Product>().Property(b => b.Description).HasMaxLength(512).IsRequired();
        }
    }
}