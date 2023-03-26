using Microsoft.AspNetCore.Identity;

namespace IdServer.Db.Models;

public class AppUser : IdentityUser
{
    public Guid OrganizationEntityIdentifier { get; set; }
    public OrganizationEntity OrganizationEntity { get; set; }
}