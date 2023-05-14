using AutoMapper;
using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.DbContexts;
using IdServer.Auth.Handlers;
using IdServer.Auth.Requirements;
using IdServer.Db;
using IdServer.Db.Models;
using IdServer.Db.RepositoryServices.InvitationRepository;
using IdServer.Db.RepositoryServices.OrganizationRepository;
using IdServer.Services;
using IdServer.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

const string allowLocalhostOrigins = "_allowLocalhostOrigin";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();

builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowLocalhostOrigins,
        policy  =>
        {
            policy.WithOrigins("https://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

var connectionString = Environment.GetEnvironmentVariable(EnvironmentVariables.IdentityServerDbConnectionString);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddIdentityServer(options =>
    {
        options.UserInteraction.LoginUrl = "login";
        options.UserInteraction.LoginReturnUrlParameter = "returnUrl";
        options.UserInteraction.LogoutUrl = "logout";
        options.UserInteraction.LogoutIdParameter = "logoutId";

        options.Events.RaiseErrorEvents = false;
        options.Events.RaiseInformationEvents = false;
        options.Events.RaiseFailureEvents = false;
        options.Events.RaiseSuccessEvents = false;

        // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes
        options.EmitStaticAudienceClaim = true;
    })
    .AddAspNetIdentity<AppUser>()
    .AddConfigurationStore(options =>
    {
        options.ResolveDbContextOptions = IdentityServerStartupHelper.ResolveDbContextOptions;
    })
    .AddOperationalStore(options =>
    {
        options.ResolveDbContextOptions = IdentityServerStartupHelper.ResolveDbContextOptions;
    })
    .AddJwtBearerClientAuthentication()
    .AddProfileService<ProfileService>();

builder.Services.AddAuthentication().AddLocalApi();

builder.Services.AddScoped<IAuthorizationHandler, AdminHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
    {
        policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme);
        policy.RequireAuthenticatedUser();

        policy.Requirements.Add(new AdminRequirement());
    });
});

var app = builder.Build();

app.UseIdentityServer();
app.UseAuthorization();


app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await services.GetRequiredService<AppDbContext>().Database.MigrateAsync();
    await services.GetRequiredService<ConfigurationDbContext>().Database.MigrateAsync();
    await services.GetRequiredService<PersistedGrantDbContext>().Database.MigrateAsync();
    
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
    var organizationRepository = scope.ServiceProvider.GetRequiredService<IOrganizationRepository>();
    
    await IdentityServerStartupHelper.AppSeed(organizationRepository, roleManager, userManager, configurationDbContext);
}

app.Run();