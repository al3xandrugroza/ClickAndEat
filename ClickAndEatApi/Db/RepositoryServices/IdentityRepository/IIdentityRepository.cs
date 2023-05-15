namespace ClickAndEatApi.Db.RepositoryServices.IdentityRepository;

public interface IIdentityRepository
{
    Task SyncWithIdentity(CancellationToken cancellationToken = default);
}