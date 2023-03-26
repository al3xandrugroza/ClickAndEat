using System.ComponentModel.DataAnnotations.Schema;

namespace ClickAndEatApi.Db.Models;

public class OrderEntity : BaseEntity
{
    public string OrderDeliverState { get; set; }
    public IEnumerable<FoodTypeEntity> FoodTypeEntities { get; set; }
    
    public Guid UserEntityId { get; set; }
    public UserEntity UserEntity { get; set; }
}