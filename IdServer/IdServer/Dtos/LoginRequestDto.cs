using System.Text.Json.Serialization;

namespace IdServer.Dtos;

public class LoginRequestDto
{
    [JsonPropertyName("UserName")]
    public string UserName { get; set; }
    
    [JsonPropertyName("Password")]
    public string Password { get; set; }

    [JsonPropertyName("IsPersistent")]
    public bool IsPersistent { get; set; } = false;
    
    [JsonPropertyName("ReturnUrl")]
    public string ReturnUrl { get; set; }
}