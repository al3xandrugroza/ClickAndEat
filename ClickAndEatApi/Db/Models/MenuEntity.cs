namespace ClickAndEatApi.Db.Models;

public class MenuEntity : BaseEntity
{
    public IEnumerable<FoodTypeEntity> FoodTypeEntities { get; set; }
}