using IdServer.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdServer.Db;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public DbSet<OrganizationEntity> OrganizationEntities { get; set; }
    
    public DbSet<InvitationEntity> InvitationEntities { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<InvitationEntity>()
            .HasKey(i => i.Identifier);
        modelBuilder.Entity<InvitationEntity>()
            .HasOne(i => i.OrganizationEntity)
            .WithMany();

        modelBuilder.Entity<OrganizationEntity>()
            .HasKey(org => org.Identifier);
        modelBuilder.Entity<OrganizationEntity>()
            .HasMany(org => org.Users)
            .WithOne(u => u.OrganizationEntity)
            .HasForeignKey(u => u.OrganizationEntityIdentifier);
    }
}