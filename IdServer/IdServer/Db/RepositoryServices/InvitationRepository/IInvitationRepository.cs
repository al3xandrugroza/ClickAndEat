using IdServer.Db.Models;
using IdServer.Dtos;

namespace IdServer.Db.RepositoryServices.InvitationRepository;

public interface IInvitationRepository
{
    Task<InvitationDto> GetByIdentifier(Guid key, CancellationToken cancellationToken);
    Task<InvitationDto> CreateInvitation(string email, string role, Guid organizationId, CancellationToken cancellationToken);
    Task DeleteAllInvitationForEmailInAllOrganizations(string email, CancellationToken cancellationToken);
}