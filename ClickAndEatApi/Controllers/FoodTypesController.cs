using ClickAndEatApi.Auth;
using ClickAndEatApi.Dtos;
using ClickAndEatApi.Services.FoodTypeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickAndEatApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = Policies.Admin)]
public class FoodTypesController : ControllerBase
{
    private readonly IFoodTypeService _foodTypeService;

    public FoodTypesController(IFoodTypeService foodTypeService)
    {
        _foodTypeService = foodTypeService;
    }

    [HttpGet("all")]
    public async Task<IEnumerable<FoodTypeDto>> GetAllFoodTypes(CancellationToken cancellationToken)
    {
        return await _foodTypeService.GetAllFoodTypes(cancellationToken);
    }

    [HttpGet("item")]
    public async Task<FoodTypeDto> GetFoodType([FromQuery] Guid identifier, CancellationToken cancellationToken)
    {
        return await _foodTypeService.GetFoodTypeByIdentifier(identifier, cancellationToken);
    }

    [HttpPost]
    public async Task<FoodTypeDto> CreateFoodType([FromBody] FoodTypeCreateRequestDto foodTypeCreateRequestDto, CancellationToken cancellationToken)
    {
        return await _foodTypeService.CreateFoodType(foodTypeCreateRequestDto, cancellationToken);
    }

    [HttpPut]
    public async Task<FoodTypeDto> UpdateFoodType([FromBody] FoodTypeUpdateRequestDto foodTypeUpdateRequestDto, CancellationToken cancellationToken)
    {
        return await _foodTypeService.UpdateFoodType(foodTypeUpdateRequestDto, cancellationToken);
    }

    [HttpDelete]
    public async Task DeleteFoodType([FromQuery] Guid identifier, CancellationToken cancellationToken)
    {
        await _foodTypeService.DeleteFoodType(identifier, cancellationToken);
    }
}
