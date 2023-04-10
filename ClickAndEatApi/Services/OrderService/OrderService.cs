using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Services.OrderService;

public class OrderService : IOrderService
{
    public async Task<OrderDto?> GetOrder()
    {
        return null;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrders()
    {
        return new List<OrderDto>
        {
            Capacity = 0
        };
    }

    public async Task<OrderDto> PlaceOrder()
    {
        return new OrderDto();
    }

    public async Task CancelOrder()
    {
    }

    public async Task DeleteAllOrders()
    {
    }
}