using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Db.RepositoryServices.ShoppingCartRepository;

public interface IShoppingCartRepository
{
    Task<ShoppingCartDto?> GetShoppingCart(CancellationToken cancellationToken);
    Task<ShoppingCartDto> CreateShoppingCart(CancellationToken cancellationToken);
    Task<ShoppingCartDto> UpdateShoppingCart(ShoppingCartDto shoppingCartDto, CancellationToken cancellationToken);
    Task ClearShoppingCart(CancellationToken cancellationToken);
    Task ClearAllShoppingCarts(CancellationToken cancellationToken);
}