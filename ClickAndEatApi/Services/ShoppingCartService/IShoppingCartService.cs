using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Services.ShoppingCartService;

public interface IShoppingCartService
{
    Task<ShoppingCartDto?> GetShoppingCart();
    Task<ShoppingCartDto> CreateShoppingCart();
    
    // Should ignore food types that are not available in the associated org menu
    // Should check that the selected items are not over the limit number specified in menu
    Task<ShoppingCartDto> UpdateShoppingCart();
    
    // Used when placing order
    Task<ShoppingCartDto> ClearShoppingCart();
    
    Task ClearAllShoppingCarts();
}