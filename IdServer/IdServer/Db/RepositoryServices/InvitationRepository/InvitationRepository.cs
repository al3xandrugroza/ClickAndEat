using IdServer.Db.Models;

namespace IdServer.Db.RepositoryServices.InvitationRepository;

public class InvitationRepository : IInvitationRepository

{
    public Task<InvitationEntity?> GetByIdentifier(Guid key, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}