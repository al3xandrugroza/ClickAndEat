using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Services.OrderService;

public interface IOrderService
{
    Task<IEnumerable<AllOrdersDto>> GetAllOrders(CancellationToken cancellationToken);
    Task<OrderDto?> GetOrder(CancellationToken cancellationToken);
    Task<OrderDto> PlaceOrder(CancellationToken cancellationToken);
    Task CancelOrder(CancellationToken cancellationToken);
    Task DeleteAllOrders(CancellationToken cancellationToken);
}