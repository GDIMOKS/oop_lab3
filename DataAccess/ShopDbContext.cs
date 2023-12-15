using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess;

public class ShopDbContext : DbContext
{
    public ShopDbContext()
    {
        
    }

    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
        
    }

    public DbSet<Shop> Shops { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ShopProduct> ShopProducts { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;UserId=postgres;Password=postgres;Database=oop_lab3");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Shop>()
            .HasMany(s => s.Products)
            .WithMany(p => p.Shops)
            .UsingEntity<ShopProduct>(
                j => j
                    .HasOne(pt => pt.Product)
                    .WithMany(t => t.ShopProducts)
                    .HasForeignKey(pt => pt.ProductName),
                j => j
                    .HasOne(pt => pt.Shop)
                    .WithMany(p => p.ShopProducts)
                    .HasForeignKey(pt => pt.ShopCode),
                j =>
                {
                    j.Property(pt => pt.Quantity).HasDefaultValue(0);
                    j.Property(pt => pt.Price).HasDefaultValue(0);
                    j.HasKey(t => new {t.ShopCode, t.ProductName});
                    j.ToTable("ShopProducts");
                });
    }
}