using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Services.MenuService;

public interface IMenuService
{
    Task<MenuDto?> GetMenu();
    Task<MenuDto> CreateMenu();
    
    // Should ignore food types that are not available on the associated org
    Task<MenuDto> UpdateMenu(MenuUpdateRequestDto menuUpdateRequestDto);
    
    Task LockMenu();
    
    Task UnlockMenu();
}