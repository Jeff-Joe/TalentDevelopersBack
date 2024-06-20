using Microsoft.EntityFrameworkCore;

namespace TalentDevelopers.Models
{
    public class TalentDevelopersContext : DbContext
    {
        public TalentDevelopersContext(DbContextOptions<TalentDevelopersContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sales> SalesTable { get; set; }

        public DbSet<Store> Stores { get; set; }
    }
}
