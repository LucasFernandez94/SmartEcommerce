using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastucture.Data
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer.Domain.Entities.Customer> Customers { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
