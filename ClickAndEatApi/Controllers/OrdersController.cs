using ClickAndEatApi.Auth;
using ClickAndEatApi.Dtos;
using ClickAndEatApi.Services.OrderService;
using ClickAndEatApi.Services.ShoppingCartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickAndEatApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("all")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IEnumerable<AllOrdersDto>> GetAllOrders(CancellationToken cancellationToken)
    {
        return await _orderService.GetAllOrders(cancellationToken);
    }

    [HttpGet]
    [Authorize(Policy = Policies.Emp)]
    public async Task<OrderDto?> GetOrder(CancellationToken cancellationToken)
    {
        return await _orderService.GetOrder(cancellationToken);
    }

    [HttpPost]
    [Authorize(Policy = Policies.Emp)]
    public async Task<OrderDto> PlaceOrder(CancellationToken cancellationToken)
    {
        return await _orderService.PlaceOrder(cancellationToken);
    }

    [HttpDelete]
    [Authorize(Policy = Policies.Emp)]
    public async Task CancelOrder(CancellationToken cancellationToken)
    {
        await _orderService.CancelOrder(cancellationToken);
    }

    [HttpDelete("all")]
    [Authorize(Policy = Policies.Admin)]
    public async Task DeleteAllOrders(CancellationToken cancellationToken)
    {
        await _orderService.DeleteAllOrders(cancellationToken);
    }
}