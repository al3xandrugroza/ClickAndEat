using ClickAndEatApi.Db.RepositoryServices.OrderRepository;
using ClickAndEatApi.Db.RepositoryServices.ShoppingCartRepository;
using ClickAndEatApi.Dtos;
using ClickAndEatApi.Services.MenuService;

namespace ClickAndEatApi.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IMenuService _menuService;
    private readonly IOrderRepository _orderRepository;

    public OrderService(IShoppingCartRepository shoppingCartRepository, IMenuService menuService, IOrderRepository orderRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _menuService = menuService;
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<AllOrdersDto>> GetAllOrders(CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllOrders(cancellationToken);
        var orderSummaryList = orders.Select(o => new AllOrdersDto
        {
            Email = o.UserEmail,
            FoodTypes = string.Join(", ", o.OrderedItems.Select(it => it.Type))
        });

        return orderSummaryList;
    }

    public async Task<OrderDto?> GetOrder(CancellationToken cancellationToken)
    {
        return await _orderRepository.GetOrder(cancellationToken);
    }

    public async Task<OrderDto> PlaceOrder(CancellationToken cancellationToken)
    {
        await _menuService.ThrowWhenLocked(cancellationToken);

        var order = await _orderRepository.PlaceOrder(cancellationToken);
        await _shoppingCartRepository.ClearShoppingCart(cancellationToken);
        return order;
    }

    public async Task CancelOrder(CancellationToken cancellationToken)
    {
        await _menuService.ThrowWhenLocked(cancellationToken);
        await _orderRepository.CancelOrder(cancellationToken);
    }

    public async Task DeleteAllOrders(CancellationToken cancellationToken)
    {
        await _menuService.ThrowWhenUnlocked(cancellationToken);
        await _orderRepository.DeleteAllOrders(cancellationToken);
        await _shoppingCartRepository.ClearAllShoppingCarts(cancellationToken);
    }
}