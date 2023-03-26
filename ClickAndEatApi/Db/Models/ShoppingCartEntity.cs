using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClickAndEatApi.Db.Models;

[Index(nameof(UserEntityId), IsUnique = true)]
public class ShoppingCartEntity : BaseEntity
{
    public IEnumerable<FoodTypeEntity> FoodTypeEntities { get; set; }
    
    [ForeignKey("UserEntity")]
    public Guid UserEntityId { get; set; }
    public UserEntity UserEntity { get; set; }
}