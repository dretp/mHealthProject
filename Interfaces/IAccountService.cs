using mHealthProject.Models.Account;

namespace mHealthProject.Interfaces;

public interface IAccountService
{
    Task<bool> CreateAccount(AccountCreateRequest req);
}