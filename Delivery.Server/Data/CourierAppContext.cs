using Delivery.Shared;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Server.Data
{
    public class CourierAppContext : DbContext
    {
        public CourierAppContext(DbContextOptions<CourierAppContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1user-many addresses
            modelBuilder.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            // 1user-many orders
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // 1order-1status
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Status)
                .WithMany()
                .HasForeignKey(o => o.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            //1 order - 1 address
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Address)
            //    .WithMany()
            //    .HasForeignKey(o => o.AddressId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
