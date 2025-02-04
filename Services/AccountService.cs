using mHealthProject.Interfaces;
using mHealthProject.Models.Account;
using mHealthProject.Utils.Account;

namespace mHealthProject.Services;

public class AccountService : IAccountService
{
    private readonly AccountUtil _accountUtil = new();

    public async Task<bool> CreateAccount(AccountCreateRequest req)
    {
        return await _accountUtil.PerformCreateAccount(req);
    }
}