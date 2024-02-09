using Microsoft.EntityFrameworkCore;

namespace products
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=sqlserver;Initial Catalog=products;user id=sa;password=Passw0rd");
        }
    }
}