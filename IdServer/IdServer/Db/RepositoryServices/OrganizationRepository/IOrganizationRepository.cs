using IdServer.Db.Models;

namespace IdServer.Db.RepositoryServices.OrganizationRepository;

public interface IOrganizationRepository
{
    Task<OrganizationEntity> CreateOrganization(string name, CancellationToken cancellationToken);
    Task<OrganizationEntity> GetByIdentifier(Guid key, CancellationToken cancellationToken);
}