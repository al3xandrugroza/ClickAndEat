namespace ClickAndEatApi.Db.Models;

public class OrderEntity : BaseEntity
{
    public IEnumerable<FoodTypeEntity> FoodTypeEntities { get; set; }
}