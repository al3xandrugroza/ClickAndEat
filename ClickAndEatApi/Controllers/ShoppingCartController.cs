using ClickAndEatApi.Dtos;
using ClickAndEatApi.Services.ShoppingCartService;
using Microsoft.AspNetCore.Mvc;

namespace ClickAndEatApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }
    
    [HttpGet]
    public async Task<ShoppingCartDto> GetShoppingCart()
    {
        return await _shoppingCartService.GetShoppingCart() ?? await _shoppingCartService.CreateShoppingCart();
    }

    [HttpPost]
    public async Task<ShoppingCartDto> UpdateShoppingCart()
    {
        return await _shoppingCartService.UpdateShoppingCart();
    }
}