using System.Text.Json.Serialization;

namespace ClickAndEatApi.Dtos;

public class MenuUpdateRequestDto
{
    [JsonPropertyName("UpdatedChoiceList")]
    public IEnumerable<FoodTypeDto> UpdatedChoiceList { get; set; }
}