using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Services.MenuService;

public interface IMenuService
{
    Task<MenuDto?> GetMenu(CancellationToken cancellationToken);
    Task<MenuDto> CreateMenu(CancellationToken cancellationToken);
    Task<MenuDto> UpdateMenu(MenuUpdateRequestDto menuUpdateRequestDto, CancellationToken cancellationToken);
    Task LockMenu(CancellationToken cancellationToken);
    Task UnlockMenu(CancellationToken cancellationToken);
    Task ThrowWhenUnlocked(CancellationToken cancellationToken);
    Task ThrowWhenLocked(CancellationToken cancellationToken);
}