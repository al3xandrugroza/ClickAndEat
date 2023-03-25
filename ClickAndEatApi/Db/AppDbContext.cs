using ClickAndEatApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace ClickAndEatApi.Db;

public class AppDbContext : DbContext
{
    public DbSet<OrganizationEntity> OrganizationEntities { get; set; }
    
    public DbSet<MenuEntity> MenuEntities { get; set; }
    public DbSet<FoodTypeEntity> FoodTypeEntities { get; set; }
    
    public DbSet<ShoppingCartEntity> ShoppingCartEntities { get; set; }
    public DbSet<OrderEntity> OrderEntities { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}