using System.ComponentModel.DataAnnotations;

namespace ClickAndEatApi.Db.Models;

public abstract  class BaseEntity
{
    [Key]
    [Required]
    public Guid Identifier { get; set; }
    
    public Guid OrganizationEntityId { get; set; }
    public OrganizationEntity OrganizationEntity { get; set; }
}