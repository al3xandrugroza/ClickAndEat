using System.Text.Json.Serialization;

namespace IdServer.Dtos;

public class AddInvitationRequestDto
{
    [JsonPropertyName("Email")]
    public string Email { get; set; }
    
    [JsonPropertyName("Role")]
    public string Role { get; set; }
}