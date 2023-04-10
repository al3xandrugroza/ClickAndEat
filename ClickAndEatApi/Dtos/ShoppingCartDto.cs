using System.Text.Json.Serialization;

namespace ClickAndEatApi.Dtos;

public class ShoppingCartDto : BaseDto
{
    [JsonPropertyName("CartItems")]
    public IEnumerable<FoodTypeDto> CartItems { get; set; }
}