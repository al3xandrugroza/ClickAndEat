using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Services.ShoppingCartService;

public class ShoppingCartService : IShoppingCartService
{
    public async Task<ShoppingCartDto?> GetShoppingCart()
    {
        return null;
    }

    public async Task<ShoppingCartDto> CreateShoppingCart()
    {
        return new ShoppingCartDto();
    }

    public async Task<ShoppingCartDto> UpdateShoppingCart()
    {
        return new ShoppingCartDto();
    }

    public async Task<ShoppingCartDto> ClearShoppingCart()
    {
        return new ShoppingCartDto();
    }

    public async Task ClearAllShoppingCarts()
    {
    }
}