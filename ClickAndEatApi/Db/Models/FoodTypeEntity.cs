namespace ClickAndEatApi.Db.Models;

public class FoodTypeEntity : BaseEntity
{
    public string Type { get; set; }
    
    public IEnumerable<MenuEntity> MenuEntities { get; set; }
    public IEnumerable<ShoppingCartEntity> ShoppingCartEntities { get; set; }
    public IEnumerable<OrderEntity> OrderEntities { get; set; }
}