using System.Text.Json.Serialization;

namespace IdServer.Dtos;

public class RegisterRequestDto
{
    [JsonPropertyName("Email")]
    public string Email { get; set; }
    
    [JsonPropertyName("Password")]
    public string Password { get; set; }
}