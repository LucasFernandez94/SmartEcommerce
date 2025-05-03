using Microsoft.EntityFrameworkCore;
using Order.Domain.Entity;

namespace Order.Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order.Domain.Entity.Order> Order { get; set; }
        public DbSet<Order.Domain.Entity.OrderProduct> OrderProduct { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order.Domain.Entity.Order>()
                .Property(p => p.PurchaseTotal)
                .HasColumnType("decimal(18,2)");
        }
    }
}
