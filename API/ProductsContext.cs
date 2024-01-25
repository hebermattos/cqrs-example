using System.Data.Entity;

namespace Products
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}