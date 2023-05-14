using ClickAndEatApi.Db.RepositoryServices.IdentityRepository;

namespace ClickAndEatApi.Middleware;


public class SyncWithIdentityMiddleware
{
    private readonly RequestDelegate _next;

    public SyncWithIdentityMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IIdentityRepository identityRepository)
    {
        await identityRepository.SyncWithIdentity();

        await _next(context);
    }
}

public static class SyncWithIdentityMiddlewareExtension
{
    public static IApplicationBuilder UseSyncWithIdentity(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SyncWithIdentityMiddleware>();
    }
}