using System.Data.Entity;

namespace ProductMVC.Models
{
    public class ProductDBContext:DbContext
    {
        public DbSet<Category>  Category { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}