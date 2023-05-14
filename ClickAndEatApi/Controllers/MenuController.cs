using ClickAndEatApi.Auth;
using ClickAndEatApi.Dtos;
using ClickAndEatApi.Services.MenuService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickAndEatApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService, ILogger<MenuController> logger)
    {
        _menuService = menuService;
    }

    [HttpGet]
    [Authorize]
    public async Task<MenuDto> GetMenu(CancellationToken cancellationToken)
    {
        return await _menuService.GetMenu(cancellationToken) ?? await _menuService.CreateMenu(cancellationToken);
    }

    [HttpPut]
    [Authorize(Policy = Policies.Admin)]
    public async Task<MenuDto> UpdateMenu([FromBody] MenuUpdateRequestDto menuUpdateRequestDto, CancellationToken cancellationToken)
    {
        return await _menuService.UpdateMenu(menuUpdateRequestDto, cancellationToken);
    }

    [HttpPost("lock")]
    [Authorize(Policy = Policies.Admin)]
    public async Task LockMenu(CancellationToken cancellationToken)
    {
        await _menuService.LockMenu(cancellationToken);
    }

    [HttpPost("unlock")]
    [Authorize(Policy = Policies.Admin)]
    public async Task UnlockMenu(CancellationToken cancellationToken)
    {
        await _menuService.UnlockMenu(cancellationToken);
    }
}