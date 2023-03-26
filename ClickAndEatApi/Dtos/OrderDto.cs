using System.Text.Json.Serialization;

namespace ClickAndEatApi.Dtos;

public class OrderDto : BaseDto
{
    [JsonPropertyName("OrderedItems")]
    public IEnumerable<FoodTypeDto> OrderedItems { get; set; }
    
    [JsonPropertyName("UserEmail")]
    public string UserEmail { get; set; }
    
    [JsonPropertyName("OrderDeliverState")]
    public string OrderDeliverState { get; set; }
}