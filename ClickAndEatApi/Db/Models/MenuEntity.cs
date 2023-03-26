using ClickAndEatApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace ClickAndEatApi.Db.Models;

[Index(nameof(OrganizationEntityId), IsUnique = true)]
public class MenuEntity : BaseEntity
{
    public int ShoppingLimit { get; set; }
    
    public IEnumerable<FoodTypeEntity> FoodTypeEntities { get; set; }
    
    public string OrderLockState { get; set; }
}