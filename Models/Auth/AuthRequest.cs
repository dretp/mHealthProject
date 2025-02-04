using System.Text.Json.Serialization;

namespace mHealthProject.Models.Auth;

public class AuthRequest
{
    [JsonPropertyName("username")] public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")] public string Password { get; set; } = string.Empty;
}