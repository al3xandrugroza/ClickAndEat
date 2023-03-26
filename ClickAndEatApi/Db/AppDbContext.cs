using ClickAndEatApi.Db.Models;
using ClickAndEatApi.Services.IdentityService;
using Microsoft.EntityFrameworkCore;

namespace ClickAndEatApi.Db;

public class AppDbContext : DbContext
{
    public DbSet<OrganizationEntity> OrganizationEntities { get; set; }
    
    public DbSet<MenuEntity> MenuEntities { get; set; }
    public DbSet<FoodTypeEntity> FoodTypeEntities { get; set; }
    
    public DbSet<ShoppingCartEntity> ShoppingCartEntities { get; set; }
    public DbSet<OrderEntity> OrderEntities { get; set; }

    private readonly IIdentityService _identityService;
    
    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IIdentityService identityService) : base(options)
    {
        _identityService = identityService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var task = _identityService.GetOrganizationId();
        task.Wait();
        var organizationId = task.Result;
        
        modelBuilder.Entity<FoodTypeEntity>().HasQueryFilter(ft => ft.OrganizationEntity.Identifier.Equals(organizationId));
        modelBuilder.Entity<MenuEntity>().HasQueryFilter(m => m.OrganizationEntity.Identifier.Equals(organizationId));
        modelBuilder.Entity<OrderEntity>().HasQueryFilter(o => o.OrganizationEntity.Identifier.Equals(organizationId));
        modelBuilder.Entity<ShoppingCartEntity>().HasQueryFilter(sc => sc.OrganizationEntity.Identifier.Equals(organizationId));
    }
}