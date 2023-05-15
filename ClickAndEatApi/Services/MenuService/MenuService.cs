using ClickAndEatApi.Db.RepositoryServices.MenuRepository;
using ClickAndEatApi.Dtos;
using ClickAndEatApi.Utils;

namespace ClickAndEatApi.Services.MenuService;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<MenuDto?> GetMenu(CancellationToken cancellationToken)
    {
        return await _menuRepository.GetMenu(cancellationToken);
    }

    public async Task<MenuDto> CreateMenu(CancellationToken cancellationToken)
    {
        return await _menuRepository.CreateMenu(cancellationToken);
    }

    public async Task<MenuDto> UpdateMenu(MenuUpdateRequestDto menuUpdateRequestDto, CancellationToken cancellationToken)
    {
        await ThrowWhenUnlocked(cancellationToken);
        return await _menuRepository.UpdateMenu(menuUpdateRequestDto, cancellationToken);
    }

    public async Task LockMenu(CancellationToken cancellationToken)
    {
        await _menuRepository.LockMenu(cancellationToken);
    }

    public async Task UnlockMenu(CancellationToken cancellationToken)
    {
        await _menuRepository.UnlockMenu(cancellationToken);
    }

    public async Task ThrowWhenUnlocked(CancellationToken cancellationToken)
    {
        var menu = await GetMenu(cancellationToken);
        if (menu?.OrderLockState == OrderLockState.Unlocked) throw new Exception("bad request");
    }

    public async Task ThrowWhenLocked(CancellationToken cancellationToken)
    {
        var menu = await GetMenu(cancellationToken);
        if (menu?.OrderLockState == OrderLockState.Locked) throw new Exception("bad request");
    }
}