using System.IdentityModel.Tokens.Jwt;
using mHealthProject.Models.User;

namespace mHealthProject.Interfaces;

public interface ISecurityService
{
    string GenerateToken(BaseUser user);
    JwtPayload RetrieveJwtPayload(string token);
}