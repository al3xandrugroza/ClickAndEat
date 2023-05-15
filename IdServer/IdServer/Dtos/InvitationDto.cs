using System.Text.Json.Serialization;

namespace IdServer.Dtos;

public class InvitationDto
{
    [JsonPropertyName("Identifier")]
    public Guid Identifier { get; set; }
    
    [JsonPropertyName("OrganizationId")]
    public Guid OrganizationId { get; set; }
    
    [JsonPropertyName("Email")]
    public string Email { get; set; }
    
    [JsonPropertyName("Role")]
    public string Role { get; set; }
}