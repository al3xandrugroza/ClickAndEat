namespace ClickAndEatApi.Db.Models;

public class ShoppingCartEntity : BaseEntity
{
    public IEnumerable<FoodTypeEntity> FoodTypeEntities { get; set; }
}