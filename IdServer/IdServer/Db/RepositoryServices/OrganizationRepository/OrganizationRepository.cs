using IdServer.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace IdServer.Db.RepositoryServices.OrganizationRepository;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly AppDbContext _context;

    public OrganizationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrganizationEntity> CreateOrganization(string name, CancellationToken cancellationToken)
    {
        var organization = new OrganizationEntity
        {
            Name = name,
            Users = new List<AppUser>()
        };

        await _context.OrganizationEntities.AddAsync(organization, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return organization;
    }

    public async Task<OrganizationEntity> GetByIdentifier(Guid key, CancellationToken cancellationToken)
    {
        return await _context.OrganizationEntities.SingleOrDefaultAsync(o => o.Identifier.Equals(key), cancellationToken) ?? throw new Exception("not found");
    }
}