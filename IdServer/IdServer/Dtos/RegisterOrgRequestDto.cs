using System.Text.Json.Serialization;

namespace IdServer.Dtos;

public class RegisterOrgRequestDto
{
    [JsonPropertyName("Email")]
    public string Email { get; set; }
    
    [JsonPropertyName("Password")]
    public string Password { get; set; }

    [JsonPropertyName("OrganizationName")]
    public string OrganizationName { get; set; }
}