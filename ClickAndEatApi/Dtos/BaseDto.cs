using System.Text.Json.Serialization;

namespace ClickAndEatApi.Dtos;

public abstract class BaseDto
{
    [JsonPropertyName("Identifier")]
    public Guid Identifier { get; set; }
}