using System.ComponentModel.DataAnnotations;

namespace ClickAndEatApi.Db.Models;

public class OrganizationEntity
{
    [Key]
    [Required]
    public Guid Identifier { get; set; }
}