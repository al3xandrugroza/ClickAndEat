using System.ComponentModel.DataAnnotations.Schema;
using ClickAndEatApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace ClickAndEatApi.Db.Models;

[Index(nameof(UserEntityId), IsUnique = true)]
public class OrderEntity : BaseEntity
{
    public string OrderDeliverState { get; set; }
    public IEnumerable<FoodTypeEntity> FoodTypeEntities { get; set; }
    
    [ForeignKey("UserEntity")]
    public Guid UserEntityId { get; set; }
    public UserEntity UserEntity { get; set; }
}