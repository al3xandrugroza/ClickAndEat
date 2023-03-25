using System.ComponentModel.DataAnnotations;

namespace IdServer.Db.Models;

public class InvitationEntity
{
    [Key]
    [Required]
    public Guid Identifier { get; set; }
    
    public string Email { get; set; }

    public OrganizationEntity OrganizationEntity { get; set; }   
}