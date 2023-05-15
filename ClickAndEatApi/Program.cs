using AutoMapper;
using ClickAndEatApi.Auth;
using ClickAndEatApi.Auth.Handlers;
using ClickAndEatApi.Auth.Requirements;
using ClickAndEatApi.Db;
using ClickAndEatApi.Db.RepositoryServices.FoodTypeRepository;
using ClickAndEatApi.Db.RepositoryServices.IdentityRepository;
using ClickAndEatApi.Db.RepositoryServices.MenuRepository;
using ClickAndEatApi.Db.RepositoryServices.OrderRepository;
using ClickAndEatApi.Db.RepositoryServices.ShoppingCartRepository;
using ClickAndEatApi.Middleware;
using ClickAndEatApi.Services.FoodTypeService;
using ClickAndEatApi.Services.MenuService;
using ClickAndEatApi.Services.OrderService;
using ClickAndEatApi.Services.ShoppingCartService;
using ClickAndEatApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IFoodTypeService, FoodTypeService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

builder.Services.AddScoped<IFoodTypeRepository, FoodTypeRepository>();
builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7255";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddScoped<IAuthorizationHandler, AdminHandler>();
builder.Services.AddScoped<IAuthorizationHandler, EmpHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.Admin, policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new AdminRequirement());
    });

    options.AddPolicy(Policies.Emp, policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new EmpRequirement());
    });
});

var connectionString = Environment.GetEnvironmentVariable(EnvironmentVariables.ClickAndEatDbConnectionString);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSyncWithIdentity();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await services.GetRequiredService<AppDbContext>().Database.MigrateAsync();
}

app.Run();
