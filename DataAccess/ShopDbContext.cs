using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ShopDbContext : DbContext
{
    public ShopDbContext()
    {
        
    }

    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
        
    }
    // DbSets
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //подключение к бд
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //настройки
    }
}