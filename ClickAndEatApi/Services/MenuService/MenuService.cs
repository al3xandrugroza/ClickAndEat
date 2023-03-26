using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Services.MenuService;

public class MenuService : IMenuService
{
    public async Task<MenuDto?> GetMenu()
    {
        return null;
    }

    public async Task<MenuDto> CreateMenu()
    {
        return new MenuDto();
    }

    public async Task<MenuDto> UpdateMenu(MenuUpdateRequestDto menuUpdateRequestDto)
    {
        return new MenuDto();
    }

    public async Task LockMenu()
    {
    }

    public async Task UnlockMenu()
    {
    }
}