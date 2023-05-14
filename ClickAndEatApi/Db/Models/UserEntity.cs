namespace ClickAndEatApi.Db.Models;

public class UserEntity : BaseEntity
{
    public OrderEntity OrderEntity { get; set; }
    public ShoppingCartEntity ShoppingCartEntity { get; set; }
    public string Email { get; set; }
}