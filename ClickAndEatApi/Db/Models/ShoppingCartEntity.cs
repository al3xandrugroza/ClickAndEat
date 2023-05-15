using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClickAndEatApi.Db.Models;

public class ShoppingCartEntity : BaseEntity
{
    public ICollection<FoodTypeEntity> FoodTypeEntities { get; set; }
    
    public Guid UserEntityId { get; set; }
    public UserEntity UserEntity { get; set; }
}