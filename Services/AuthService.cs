using mHealthProject.Interfaces;
using mHealthProject.Models.Auth;
using mHealthProject.Utils.Auth;

namespace mHealthProject.Services;

public class AuthService : IAuthService
{
    private ISecurityService _secService = new SecurityService();
    private readonly AuthUtil _util;

    public AuthService()
    {
        _secService = new SecurityService();
        _util = new AuthUtil();
    }
    
    public async Task<AuthResponse> Authenticate(AuthRequest request)
    {
        try
        {
            var userDetails = await _util.PerformAuth(request);
            var token = secSvc().GenerateToken(userDetails);

            return new AuthResponse()
            {
                Token = token,
                User = userDetails
            };
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    
    
    private ISecurityService secSvc()
    {
        return _secService ??= new SecurityService();   
    }
    
}