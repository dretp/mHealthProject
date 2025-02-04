using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using mHealthProject.Interfaces;
using mHealthProject.Models.Configuration;
using mHealthProject.Models.User;
using Microsoft.IdentityModel.Tokens;

namespace mHealthProject.Services;

public class SecurityService : ISecurityService
{
    private readonly MHealthConfiguration _config;
    private static SecurityService Instance;
    private readonly JwtSecurityTokenHandler _handler;

    public SecurityService()
    {
        Init();
        _config = MHealthConfiguration.Instance;
        _handler = new JwtSecurityTokenHandler();
    }

    private void Init()
    {
        Instance ??= this;
    }

    public SecurityService GetInstance()
    {
        return Instance;
    }
    
    public string GenerateToken(BaseUser user)
    {
        return generateJwt(user);
    }

    public JwtPayload RetrieveJwtPayload(string token)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var secToken = _handler.ReadJwtToken(token);

            var payload = secToken.Payload;

            if (payload == null)
                return payload;
            
            //check if the token is still active, if not throw security error 
            // if(secToken.ValidTo < DateTime.UtcNow)
            //     throw new SecurityTokenException("Invalid token");

            if (string.IsNullOrWhiteSpace(payload.GetValueOrDefault("userId").ToString()))
            {
                throw  new Exception("Invalid token");
            }
            return payload;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while retrieving JWT payload {e.StackTrace}");
            throw new Exception("Unable to retrieve token");
        }
    }
    
    private string generateJwt(BaseUser user)
    {
        //generate token that is valid for 3 hours
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config.JwtSecret);

        var userType = (int)user.UserType;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("userId", user.Id.ToString()),
                new Claim("userType", userType.ToString()),
                new Claim("userName", user.Username),
                new Claim("name", user.FirstName + " " + user.LastName)
            ]),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}