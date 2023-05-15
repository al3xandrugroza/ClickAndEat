using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using IdServer.Db.Models;
using IdServer.Db.RepositoryServices.OrganizationRepository;
using IdServer.Utils;
using Microsoft.AspNetCore.Identity;

namespace IdServer.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IOrganizationRepository _organizationRepository;

    public ProfileService(UserManager<AppUser> userManager, IOrganizationRepository organizationRepository)
    {
        _userManager = userManager;
        _organizationRepository = organizationRepository;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var claims = new List<Claim>();

        var subjectId = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(subjectId);

        claims.Add(new Claim(JwtClaimTypes.Email, user.Email));
        claims.Add(new Claim(AppClaims.OrganizationId, user.OrganizationEntityIdentifier.ToString()));

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.First();

        claims.Add(new Claim(JwtClaimTypes.Role, role));

        var organizationEntity = await _organizationRepository.GetByIdentifier(user.OrganizationEntityIdentifier, CancellationToken.None);
        var organizationName = organizationEntity.Name;

        claims.Add(new Claim(AppClaims.OrganizationName, organizationName));

        context.IssuedClaims.AddRange(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var subjectId = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(subjectId);

        context.IsActive = user is not null;
    }
}