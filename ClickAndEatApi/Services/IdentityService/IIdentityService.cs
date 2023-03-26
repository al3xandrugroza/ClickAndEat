namespace ClickAndEatApi.Services.IdentityService;

public interface IIdentityService
{
    Task<string> GetOrganizationId();
}