using mHealthProject.Interfaces;
using mHealthProject.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace mHealthProject.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(ILogger<AuthController> logger, IAuthService authService) : Controller
{
    [HttpPost("")]
    public async Task<IActionResult> PerformAuth([FromBody] AuthRequest authRequest)
    {
        try
        {
            var response = await authService.Authenticate(authRequest);
        
            return Ok(response);
        }
        catch (Exception e)
        {
            return Unauthorized("Invalid username or password");
        }
    }
}