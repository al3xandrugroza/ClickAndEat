using ClickAndEatApi.Dtos;
using ClickAndEatApi.Services.MenuService;
using Microsoft.AspNetCore.Mvc;

namespace ClickAndEatApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;
    
    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet]
    public async Task<MenuDto> GetMenu()
    {
        return await _menuService.GetMenu() ?? await _menuService.CreateMenu();
    }

    [HttpPut]
    public async Task<MenuDto> UpdateMenu([FromBody] MenuUpdateRequestDto menuUpdateRequestDto)
    {
        return await _menuService.UpdateMenu(menuUpdateRequestDto);
    }

    [HttpPost("lock")]
    public async Task LockMenu()
    {
        // Should block all shopping cart / orders operations
        // Make a policy to enforce these one
        await _menuService.LockMenu();
    }
    
    [HttpPost("unlock")]
    public async Task UnlockMenu()
    {
        await _menuService.UnlockMenu();
    }
}