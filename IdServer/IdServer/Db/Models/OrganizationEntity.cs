using System.ComponentModel.DataAnnotations;

namespace IdServer.Db.Models;

public class OrganizationEntity
{
    public Guid Identifier { get; set; }

    public string Name { get; set; }

    public IEnumerable<AppUser> Users { get; set; }
}