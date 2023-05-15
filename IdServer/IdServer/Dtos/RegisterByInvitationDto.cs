using System.Text.Json.Serialization;

namespace IdServer.Dtos;

public class RegisterByInvitationDto
{
    [JsonPropertyName("Password")]
    public string Password { get; set; }
}