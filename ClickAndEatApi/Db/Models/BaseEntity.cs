using System.ComponentModel.DataAnnotations;

namespace ClickAndEatApi.Db.Models;

public class BaseEntity
{
    [Key]
    [Required]
    public Guid Identifier { get; set; }
    
    public OrganizationEntity OrganizationEntity { get; set; }
}