using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Services.OrderService;

public interface IOrderService
{ 
    Task<OrderDto?> GetOrder();
    
    // Get all orders for users with non-Admin role
    Task<IEnumerable<OrderDto>> GetAllOrders();
    
    // Should clear the cart
    // Should notify the user by sending an email
    Task<OrderDto> PlaceOrder();
    
    // Should notify the user by sending an email
    Task CancelOrder();

    Task DeleteAllOrders();
}