using IdServer.Db.Models;

namespace IdServer.Db.RepositoryServices.InvitationRepository;

public interface IInvitationRepository
{
    Task<InvitationEntity?> GetByIdentifier(Guid key, CancellationToken cancellationToken);
}