using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Services.FoodTypeService;

public interface IFoodTypeService
{
    Task<IEnumerable<FoodTypeDto>> GetAllFoodTypes();
    Task<FoodTypeDto> GetFoodTypeByIdentifier(Guid identifier);
    Task<FoodTypeDto> CreateFoodType(FoodTypeCreateRequestDto foodTypeCreateRequestDto);
    Task<FoodTypeDto> UpdateFoodType(FoodTypeUpdateRequestDto foodTypeUpdateRequestDto);
    Task DeleteFoodType(Guid identifier);
}