using ClickAndEatApi.Db.RepositoryServices.ShoppingCartRepository;
using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Services.ShoppingCartService;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
    }

    public async Task<ShoppingCartDto?> GetShoppingCart(CancellationToken cancellationToken)
    {
        return await _shoppingCartRepository.GetShoppingCart(cancellationToken);
    }

    public async Task<ShoppingCartDto> CreateShoppingCart(CancellationToken cancellationToken)
    {
        return await _shoppingCartRepository.CreateShoppingCart(cancellationToken);
    }

    public async Task<ShoppingCartDto> UpdateShoppingCart(ShoppingCartDto shoppingCartDto, CancellationToken cancellationToken)
    {
        return await _shoppingCartRepository.UpdateShoppingCart(shoppingCartDto, cancellationToken);
    }

    public async Task ClearShoppingCart(CancellationToken cancellationToken)
    {
        await _shoppingCartRepository.ClearShoppingCart(cancellationToken);
    }

    public async Task ClearAllShoppingCarts(CancellationToken cancellationToken)
    {
        await _shoppingCartRepository.ClearAllShoppingCarts(cancellationToken);
    }
}