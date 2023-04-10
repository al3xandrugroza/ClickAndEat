using ClickAndEatApi.Dtos;
using ClickAndEatApi.Services.OrderService;
using ClickAndEatApi.Services.ShoppingCartService;
using Microsoft.AspNetCore.Mvc;

namespace ClickAndEatApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IShoppingCartService _shoppingCartService;
    
    public OrdersController(IOrderService orderService,
        IShoppingCartService shoppingCartService)
    {
        _orderService = orderService;
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet("all")]
    public async Task<IEnumerable<OrderDto>> GetAllOrders()
    {
        // For admins only
        return await _orderService.GetAllOrders();
    }
    
    [HttpGet]
    public async Task<OrderDto?> GetOrder()
    {
        // For admins only
        return await _orderService.GetOrder();
    }

    [HttpPost]
    public async Task<OrderDto> PlaceOrder()
    {
        return await _orderService.PlaceOrder();
    }

    [HttpDelete]
    public async Task CancelOrder()
    {
        await _orderService.CancelOrder();
    }
    
    [HttpDelete("all")]
    public async Task DeleteAllOrders()
    {
        // Should be performed only when menu is locked
        // Make a policy to enforce this behaviour
        await _orderService.DeleteAllOrders();

        await _shoppingCartService.ClearAllShoppingCarts();
    }
}