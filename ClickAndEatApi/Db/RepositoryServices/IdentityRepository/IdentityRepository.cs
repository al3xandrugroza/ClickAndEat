using ClickAndEatApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace ClickAndEatApi.Db.RepositoryServices.IdentityRepository;

public class IdentityRepository : IIdentityRepository
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _organizationId;
    private readonly string _userId;
    private readonly string _userEmail;

    public IdentityRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;

        _organizationId = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("org_id"))?.Value;
        _userId = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
        _userEmail = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("email"))?.Value;
    }

    public async Task SyncWithIdentity(CancellationToken cancellationToken)
    {
        await TryCreateOrganization(cancellationToken);
        await TryCreateUser(cancellationToken);
    }
    
    private async Task TryCreateOrganization(CancellationToken cancellationToken)
    {
        var organizationEntity = await _context.OrganizationEntities.SingleOrDefaultAsync(user => user.Identifier.Equals(new Guid(_organizationId)), cancellationToken);
        if (organizationEntity is not null) return;

        var newOrganizationEntity = new OrganizationEntity
        {
            Identifier = new Guid(_organizationId)
        };

        await _context.AddAsync(newOrganizationEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    private async Task TryCreateUser(CancellationToken cancellationToken)
    {
        var userEntity = await _context.UserEntities.SingleOrDefaultAsync(user => user.Identifier.Equals(new Guid(_userId)), cancellationToken);
        if (userEntity is not null) return;

        var newUserEntity = new UserEntity
        {
            Identifier = new Guid(_userId),
            OrganizationEntityId = new Guid(_organizationId),
            Email = _userEmail
        };

        await _context.AddAsync(newUserEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}