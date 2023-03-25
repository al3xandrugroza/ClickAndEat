using Microsoft.AspNetCore.Identity;

namespace IdServer.Db.Models;

public class AppUser : IdentityUser
{
    public OrganizationEntity OrganizationEntity { get; set; }
}