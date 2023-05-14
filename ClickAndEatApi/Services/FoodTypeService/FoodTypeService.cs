using ClickAndEatApi.Db.RepositoryServices.FoodTypeRepository;
using ClickAndEatApi.Dtos;
using ClickAndEatApi.Services.MenuService;

namespace ClickAndEatApi.Services.FoodTypeService;

public class FoodTypeService : IFoodTypeService
{
    private readonly IFoodTypeRepository _foodTypeRepository;
    private readonly IMenuService _menuService;

    public FoodTypeService(IMenuService menuService, IFoodTypeRepository foodTypeRepository)
    {
        _menuService = menuService;
        _foodTypeRepository = foodTypeRepository;
    }

    public async Task<IEnumerable<FoodTypeDto>> GetAllFoodTypes(CancellationToken cancellationToken)
    {
        return await _foodTypeRepository.GetAllFoodTypes(cancellationToken);
    }

    public async Task<FoodTypeDto> GetFoodTypeByIdentifier(Guid identifier, CancellationToken cancellationToken)
    {
        return await _foodTypeRepository.GetFoodTypeByIdentifier(identifier, cancellationToken);
    }

    public async Task<FoodTypeDto> CreateFoodType(FoodTypeCreateRequestDto foodTypeCreateRequestDto, CancellationToken cancellationToken)
    {
        return await _foodTypeRepository.CreateFoodType(foodTypeCreateRequestDto, cancellationToken);
    }

    public async Task<FoodTypeDto> UpdateFoodType(FoodTypeUpdateRequestDto foodTypeUpdateRequestDto, CancellationToken cancellationToken)
    {
        await _menuService.ThrowWhenUnlocked(cancellationToken);
        return await _foodTypeRepository.UpdateFoodType(foodTypeUpdateRequestDto, cancellationToken);
    }

    public async Task DeleteFoodType(Guid identifier, CancellationToken cancellationToken)
    {
        await _menuService.ThrowWhenUnlocked(cancellationToken);
        await _foodTypeRepository.DeleteFoodType(identifier, cancellationToken);
    }
}