using System.ComponentModel.DataAnnotations;

namespace IdServer.Db.Models;

public class InvitationEntity
{
    public Guid Identifier { get; set; }
    
    public string Email { get; set; }

    public OrganizationEntity OrganizationEntity { get; set; }   
}