using mHealthProject.Models.User;

namespace mHealthProject.Models.Auth;

public class AuthResponse()
{
    
    public string Token { get; set; } = string.Empty;
    public BaseUser User { get; set; }
}