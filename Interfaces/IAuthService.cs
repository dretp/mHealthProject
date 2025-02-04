using mHealthProject.Models.Auth;

namespace mHealthProject.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> Authenticate(AuthRequest request);
}