using ClickAndEatApi.Dtos;
using ClickAndEatApi.Services.FoodTypeService;
using Microsoft.AspNetCore.Mvc;

namespace ClickAndEatApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodTypesController : ControllerBase
{
    private readonly ILogger<FoodTypesController> _logger;
    private readonly IFoodTypeService _foodTypeService;

    public FoodTypesController(
        ILogger<FoodTypesController> logger,
        IFoodTypeService foodTypeService)
    {
        _logger = logger;
        _foodTypeService = foodTypeService;
    }

    [HttpGet("all")]
    public async Task<IEnumerable<FoodTypeDto>> GetAllFoodTypes()
    {
        return await _foodTypeService.GetAllFoodTypes();
    }

    [HttpGet("item")]
    public async Task<FoodTypeDto> GetFoodType([FromQuery] Guid identifier)
    {
        return await _foodTypeService.GetFoodTypeByIdentifier(identifier);
    }
    
    [HttpPost]
    public async Task<FoodTypeDto> CreateFoodType([FromBody] FoodTypeCreateRequestDto foodTypeCreateRequestDto)
    {
        return await _foodTypeService.CreateFoodType(foodTypeCreateRequestDto);
    }

    [HttpPut]
    public async Task<FoodTypeDto> UpdateFoodType([FromBody] FoodTypeUpdateRequestDto foodTypeUpdateRequestDto)
    {
        return await _foodTypeService.UpdateFoodType(foodTypeUpdateRequestDto);
    }

    [HttpDelete]
    public async Task DeleteFoodType([FromQuery] Guid identifier)
    {
        await _foodTypeService.DeleteFoodType(identifier);
    }
}
