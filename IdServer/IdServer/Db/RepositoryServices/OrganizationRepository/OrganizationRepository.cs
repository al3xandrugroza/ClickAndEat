using IdServer.Db.Models;

namespace IdServer.Db.RepositoryServices.OrganizationRepository;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly AppDbContext _context;

    public OrganizationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrganizationEntity> CreateOrganization(CancellationToken cancellationToken)
    {
        var organization = new OrganizationEntity
        {
            Users = new List<AppUser>()
        };

        await _context.OrganizationEntities.AddAsync(organization, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return organization;
    }

    public async Task<OrganizationEntity> GetByIdentifier(Guid key, CancellationToken cancellationToken)
    {
        return _context.OrganizationEntities.Single(o => o.Identifier.Equals(key));
    }
}