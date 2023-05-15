using ClickAndEatApi.Auth.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace ClickAndEatApi.Auth.Handlers;

public class AdminHandler : AuthorizationHandler<AdminRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
        const string type = "role";
        var role = context.User.Claims.SingleOrDefault(c => c.Type.Contains(type))?.Value;
        
        if (role is not null && role.Equals(Roles.Admin)) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}