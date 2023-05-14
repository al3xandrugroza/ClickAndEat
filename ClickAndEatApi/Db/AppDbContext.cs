using ClickAndEatApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace ClickAndEatApi.Db;

public class AppDbContext : DbContext
{
    public DbSet<OrganizationEntity> OrganizationEntities { get; set; }
    public DbSet<UserEntity> UserEntities { get; set; }

    public DbSet<MenuEntity> MenuEntities { get; set; }
    public DbSet<FoodTypeEntity> FoodTypeEntities { get; set; }

    public DbSet<ShoppingCartEntity> ShoppingCartEntities { get; set; }
    public DbSet<OrderEntity> OrderEntities { get; set; }

    private readonly string _organizationId;

    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _organizationId = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("org_id"))?.Value;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodTypeEntity>()
            .HasOne(ft => ft.OrganizationEntity)
            .WithMany()
            .HasForeignKey(ft => ft.OrganizationEntityId);
        modelBuilder.Entity<FoodTypeEntity>()
            .HasKey(ft => ft.Identifier);
        modelBuilder.Entity<FoodTypeEntity>()
            .HasMany(ft => ft.MenuEntities)
            .WithMany(m => m.FoodTypeEntities);
        modelBuilder.Entity<FoodTypeEntity>()
            .HasMany(ft => ft.ShoppingCartEntities)
            .WithMany(sc => sc.FoodTypeEntities);
        modelBuilder.Entity<FoodTypeEntity>()
            .HasMany(ft => ft.OrderEntities)
            .WithMany(o => o.FoodTypeEntities);
        modelBuilder.Entity<FoodTypeEntity>()
            .HasIndex(ft => new { ft.Type, ft.OrganizationEntityId })
            .IsUnique();

        modelBuilder.Entity<MenuEntity>()
            .HasOne(m => m.OrganizationEntity)
            .WithMany()
            .HasForeignKey(m => m.OrganizationEntityId);
        modelBuilder.Entity<MenuEntity>()
            .HasKey(m => m.Identifier);
        modelBuilder.Entity<MenuEntity>()
            .HasIndex(m => m.OrganizationEntityId)
            .IsUnique();

        modelBuilder.Entity<OrderEntity>()
            .HasOne(o => o.OrganizationEntity)
            .WithMany()
            .HasForeignKey(o => o.OrganizationEntityId);
        modelBuilder.Entity<OrderEntity>()
            .HasKey(o => o.Identifier);
        modelBuilder.Entity<OrderEntity>()
            .HasOne(o => o.UserEntity)
            .WithOne(u => u.OrderEntity)
            .HasForeignKey<OrderEntity>(o => o.UserEntityId);
        modelBuilder.Entity<OrderEntity>()
            .HasIndex(o => o.UserEntityId)
            .IsUnique();

        modelBuilder.Entity<OrganizationEntity>()
            .HasKey(org => org.Identifier);

        modelBuilder.Entity<ShoppingCartEntity>()
            .HasOne(sc => sc.OrganizationEntity)
            .WithMany()
            .HasForeignKey(sc => sc.OrganizationEntityId);
        modelBuilder.Entity<ShoppingCartEntity>()
            .HasKey(sc => sc.Identifier);
        modelBuilder.Entity<ShoppingCartEntity>()
            .HasOne(sc => sc.UserEntity)
            .WithOne(u => u.ShoppingCartEntity)
            .HasForeignKey<ShoppingCartEntity>(sc => sc.UserEntityId);
        modelBuilder.Entity<ShoppingCartEntity>()
            .HasIndex(sc => sc.UserEntityId)
            .IsUnique();

        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.OrganizationEntity)
            .WithMany()
            .HasForeignKey(u => u.OrganizationEntityId);
        modelBuilder.Entity<UserEntity>()
            .HasKey(u => u.Identifier);

        modelBuilder.Entity<FoodTypeEntity>().HasQueryFilter(ft => ft.OrganizationEntity.Identifier.Equals(new Guid(_organizationId)));
        modelBuilder.Entity<MenuEntity>().HasQueryFilter(m => m.OrganizationEntity.Identifier.Equals(new Guid(_organizationId)));
        modelBuilder.Entity<OrderEntity>().HasQueryFilter(o => o.OrganizationEntity.Identifier.Equals(new Guid(_organizationId)));
        modelBuilder.Entity<ShoppingCartEntity>().HasQueryFilter(sc => sc.OrganizationEntity.Identifier.Equals(new Guid(_organizationId)));
    }
}