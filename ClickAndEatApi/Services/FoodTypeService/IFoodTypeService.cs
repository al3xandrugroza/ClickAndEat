using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Services.FoodTypeService;

public interface IFoodTypeService
{
    Task<IEnumerable<FoodTypeDto>> GetAllFoodTypes(CancellationToken cancellationToken);
    Task<FoodTypeDto> GetFoodTypeByIdentifier(Guid identifier, CancellationToken cancellationToken);
    Task<FoodTypeDto> CreateFoodType(FoodTypeCreateRequestDto foodTypeCreateRequestDto, CancellationToken cancellationToken);
    Task<FoodTypeDto> UpdateFoodType(FoodTypeUpdateRequestDto foodTypeUpdateRequestDto, CancellationToken cancellationToken);
    Task DeleteFoodType(Guid identifier, CancellationToken cancellationToken);
}