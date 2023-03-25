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
}