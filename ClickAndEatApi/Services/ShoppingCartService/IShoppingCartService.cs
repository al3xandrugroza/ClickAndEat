using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Services.ShoppingCartService;

public interface IShoppingCartService
{
    Task<ShoppingCartDto?> GetShoppingCart(CancellationToken cancellationToken);
    Task<ShoppingCartDto> CreateShoppingCart(CancellationToken cancellationToken);
    Task<ShoppingCartDto> UpdateShoppingCart(ShoppingCartDto shoppingCartDto, CancellationToken cancellationToken);
    Task ClearShoppingCart(CancellationToken cancellationToken);
    Task ClearAllShoppingCarts(CancellationToken cancellationToken);
}