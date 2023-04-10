using System.Text.Json.Serialization;
using ClickAndEatApi.Utils;

namespace ClickAndEatApi.Dtos;

public class MenuDto : BaseDto
{
    [JsonPropertyName("ShoppingLimit")]
    public int ShoppingLimit { get; set; }
    
    [JsonPropertyName("ChoiceList")]
    public IEnumerable<FoodTypeDto> ChoiceList { get; set; }
    
    [JsonPropertyName("OrderLockState")]
    public string OrderLockState { get; set; }
}