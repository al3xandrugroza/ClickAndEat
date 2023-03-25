using System.Security.Claims;
using IdentityModel;
using IdServer.Db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IdServer.Utils;

public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser>
{
    private readonly UserManager<AppUser> _userManager;
    
    public AppUserClaimsPrincipalFactory(UserManager<AppUser> userManager, IOptions<IdentityOptions> optionsAccessor) :
        base(userManager, optionsAccessor)
    {
        _userManager = userManager;
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
    {
        var claims = await base.GenerateClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddClaim(new Claim(JwtClaimTypes.Role, roles.FirstOrDefault() ?? string.Empty));

        return claims;
    }
}