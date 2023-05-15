using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Db.RepositoryServices.MenuRepository;

public interface IMenuRepository
{
    Task<MenuDto?> GetMenu(CancellationToken cancellationToken);
    Task<MenuDto> CreateMenu(CancellationToken cancellationToken);
    Task<MenuDto> UpdateMenu(MenuUpdateRequestDto menuUpdateRequestDto, CancellationToken cancellationToken);
    Task LockMenu(CancellationToken cancellationToken);
    Task UnlockMenu(CancellationToken cancellationToken);
}