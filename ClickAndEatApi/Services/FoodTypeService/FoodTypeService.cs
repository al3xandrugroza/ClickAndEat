using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Services.FoodTypeService;

public class FoodTypeService : IFoodTypeService
{
    public Task<IEnumerable<FoodTypeDto>> GetAllFoodTypes()
    {
        throw new NotImplementedException();
    }

    public Task<FoodTypeDto> GetFoodTypeByIdentifier(Guid identifier)
    {
        throw new NotImplementedException();
    }

    public Task<FoodTypeDto> CreateFoodType(FoodTypeCreateRequestDto foodTypeCreateRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<FoodTypeDto> UpdateFoodType(FoodTypeUpdateRequestDto foodTypeUpdateRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteFoodType(Guid identifier)
    {
        throw new NotImplementedException();
    }
}