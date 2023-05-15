using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Db.RepositoryServices.OrderRepository;

public interface IOrderRepository
{
    Task<IEnumerable<OrderDto>> GetAllOrders(CancellationToken cancellationToken);
    Task<OrderDto?> GetOrder(CancellationToken cancellationToken);
    Task<OrderDto> PlaceOrder(CancellationToken cancellationToken);
    Task CancelOrder(CancellationToken cancellationToken);
    Task DeleteAllOrders(CancellationToken cancellationToken);
}