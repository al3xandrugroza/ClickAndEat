using System.Text.Json.Serialization;

namespace ClickAndEatApi.Dtos;

public class AllOrdersDto
{
    [JsonPropertyName("Email")]
    public string Email { get; set; }

    [JsonPropertyName("FoodTypes")]
    public string FoodTypes { get; set; }
}