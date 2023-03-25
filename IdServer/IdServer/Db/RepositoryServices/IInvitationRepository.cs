using IdServer.Db.Models;

namespace IdServer.Db.RepositoryServices;

public interface IInvitationRepository
{
    Task<InvitationEntity> GetByIdentifier(Guid key, CancellationToken cancellationToken);
}