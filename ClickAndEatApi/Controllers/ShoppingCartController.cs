using ClickAndEatApi.Auth;
using ClickAndEatApi.Dtos;
using ClickAndEatApi.Services.OrderService;
using ClickAndEatApi.Services.ShoppingCartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickAndEatApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;
    private readonly IOrderService _orderService;

    public ShoppingCartController(IShoppingCartService shoppingCartService, IOrderService orderService)
    {
        _shoppingCartService = shoppingCartService;
        _orderService = orderService;
    }

    [HttpGet]
    [Authorize(Policy = Policies.Emp)]
    public async Task<ShoppingCartDto> GetShoppingCart(CancellationToken cancellationToken)
    {
        return await _shoppingCartService.GetShoppingCart(cancellationToken) ?? await _shoppingCartService.CreateShoppingCart(cancellationToken);
    }

    [HttpPut]
    [Authorize(Policy = Policies.Emp)]
    public async Task<ShoppingCartDto> UpdateShoppingCart([FromBody] ShoppingCartDto shoppingCartDto, CancellationToken cancellationToken)
    {
        if (await _orderService.GetOrder(cancellationToken) is not null) throw new Exception("bad req");
        return await _shoppingCartService.UpdateShoppingCart(shoppingCartDto, cancellationToken);
    }
}