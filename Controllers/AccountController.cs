using mHealthProject.Interfaces;
using mHealthProject.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace mHealthProject.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(ILogger<AccountController> logger, IAccountService svc) : Controller
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAccountDetails(int id)
    {
        return Ok(id);
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreateAccount(AccountCreateRequest req)
    {
        var result = await svc.CreateAccount(req);
        
        return Ok(result);
    }
    
}