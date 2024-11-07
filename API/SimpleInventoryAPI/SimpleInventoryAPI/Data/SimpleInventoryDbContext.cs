using Microsoft.EntityFrameworkCore;
using SimpleInventoryAPI.Models;

namespace SimpleInventoryAPI.Data
{
    public class SimpleInventoryDbContext : DbContext
    {
        public SimpleInventoryDbContext(DbContextOptions<SimpleInventoryDbContext> options) : base(options)
        {
            
        }

        public DbSet<People> Peoples { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryMoveItem> inventoryMoveItems { get; set; }
        public DbSet<InventoryMove> inventoryMoves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryMove>()
                .HasMany(e => e.Items)
                .WithOne(e => e.InventoryMove)
                .HasForeignKey(e => e.InventoryMoveRefId)
                .HasPrincipalKey(e => e.Id);
            modelBuilder.Entity<InventoryMove>()
                .HasOne(e => e.People);

            modelBuilder.Entity<InventoryMoveItem>()
                .HasOne(e => e.Product);
        }

    }
}
