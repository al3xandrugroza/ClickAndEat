namespace ClickAndEatApi.Db.Models;

public class MenuEntity : BaseEntity
{
    public int ShoppingLimit { get; set; }
    
    public IEnumerable<FoodTypeEntity> FoodTypeEntities { get; set; }
    
    public string OrderLockState { get; set; }
}