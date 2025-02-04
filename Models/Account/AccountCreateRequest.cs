using System.Text.Json.Serialization;
using mHealthProject.Models.User;

namespace mHealthProject.Models.Account;

public class AccountCreateRequest()
{
    [JsonPropertyName("user")]
    public string Username { get; set; } = string.Empty;
    
    [JsonPropertyName("userType")]
    public UserTypeEnum UserType { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
    
    [JsonPropertyName("firstname")]
    public string Firstname { get; set; } = string.Empty;
    
    [JsonPropertyName("lastname")]
    public string Lastname { get; set; } = string.Empty;
    
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
}