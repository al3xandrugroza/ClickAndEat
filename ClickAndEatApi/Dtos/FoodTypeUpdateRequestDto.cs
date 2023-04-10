using System.Text.Json.Serialization;

namespace ClickAndEatApi.Dtos;

public class FoodTypeUpdateRequestDto
{
    [JsonPropertyName("Identifier")]
    public Guid Identifier { get; set; }
    
    [JsonPropertyName("Type")]
    public string Type { get; set; }
    
    [JsonPropertyName("Description")]
    public string Description { get; set; }
    
    [JsonPropertyName("ImageLink")]
    public string ImageLink { get; set; } 
}