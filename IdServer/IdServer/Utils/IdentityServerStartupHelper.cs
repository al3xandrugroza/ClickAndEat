using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using IdServer.Db.Models;
using IdServer.Db.RepositoryServices.OrganizationRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdServer.Utils;

public static class IdentityServerStartupHelper
{
    private const string CurrentAssemblyName = "IdServer";

    public static void ResolveDbContextOptions(IServiceProvider serviceProvider, DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable(EnvironmentVariables.IdentityServerDbConnectionString);
        optionsBuilder.UseSqlServer(connectionString ?? throw new InvalidOperationException(), b => b.MigrationsAssembly(CurrentAssemblyName));
    }
    
    public static async Task AppSeed(IOrganizationRepository organizationRepository, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, ConfigurationDbContext configurationDbContext)
    {
        await roleManager.SeedRoles();
        await userManager.SeedUsers(organizationRepository);
        await configurationDbContext.SeedClients();
        await configurationDbContext.SeedIdentityResources();
    }

    private static async Task SeedIdentityResources(this ConfigurationDbContext configurationDbContext)
    {
        if (!await configurationDbContext.IdentityResources.AnyAsync())
        {
            await configurationDbContext.IdentityResources.AddRangeAsync(
                new IdentityResources.OpenId().ToEntity(),
                new IdentityResources.Profile().ToEntity(),
                new IdentityResources.Email().ToEntity());

            await configurationDbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedRoles(this RoleManager<IdentityRole> roleManager)
    {
        await roleManager.SeedRole(RoleType.Admin);
        await roleManager.SeedRole(RoleType.Emp);
    }

    private static async Task SeedRole(this RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (! await roleManager.RoleExistsAsync(roleName))
        {
            var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (!roleResult.Succeeded)
            {
                throw new Exception("Failed to create role");
            }
        }
    }

    private static async Task SeedUsers(this UserManager<AppUser> userManager, IOrganizationRepository organizationRepository)
    {
        var organizationEntity = await organizationRepository.CreateOrganization(CancellationToken.None);
        // development seed | al3xTODO: del user seed (optional)
        await userManager.SeedUser("admin@outlook.com", RoleType.Admin, organizationEntity);
        await userManager.SeedUser("emp@outlook.com", RoleType.Emp, organizationEntity);
    }

    private static async Task SeedUser(this UserManager<AppUser> userManager, string email, string roleName, OrganizationEntity organizationEntity)
    {
        var password = "y0urStrong(!)Password";
        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new AppUser
            {
                UserName = email,
                Email = email,
                OrganizationEntity = organizationEntity
            };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create user");
            }
            
            result = await userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to add user to role");
            }
        }
    }

    private static async Task SeedClients(this ConfigurationDbContext configurationDbContext)
    {
        if (!await configurationDbContext.Clients.AnyAsync())
        {
            await configurationDbContext.Clients.AddRangeAsync(
                new Client
                {
                    ClientId = "web",
                    
                    RequireClientSecret = false,
                    
                    AllowedGrantTypes = GrantTypes.Code,
                    
                    RedirectUris = new List<string> { "https://localhost:4200/" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },
                    
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "email" }
                }.ToEntity());

            await configurationDbContext.SaveChangesAsync();
        }
    }
}