using System.Text;
using mHealthProject.Models.Account;
using mHealthProject.Utils.Base;

namespace mHealthProject.Utils.Account;

public class AccountUtil : BaseUtil
{

    public async Task<bool> PerformCreateAccount(AccountCreateRequest req)
    {
        return await CreateAccount(req);
    }



    private async Task<bool> CreateAccount(AccountCreateRequest req)
    {
        var pwd = encryptPassword(req.Password);
        var sql = new StringBuilder();
        sql.Append("INSERT INTO MpathHealth.dbo.[user] (user_type_id, username, first_name, last_name, email, password)");
        sql.Append("VALUES (@userType, @username, @first, @last, @email, @pass);");

        try
        {
            await using var conn = connection();
            var cmd = command(sql.ToString(), conn);

            cmd.Parameters.AddWithValue("userType", req.UserType);
            cmd.Parameters.AddWithValue("username", req.Username.ToLower());
            cmd.Parameters.AddWithValue("first", req.Firstname);
            cmd.Parameters.AddWithValue("last", req.Lastname);
            cmd.Parameters.AddWithValue("email", req.Email);
            cmd.Parameters.AddWithValue("pass", pwd);

            await cmd.ExecuteNonQueryAsync();
        
            return true;
        }
        catch (Exception e)
        {
            LogError(e, "AccountUtil.CreateAccount");
            throw new Exception("Unable to create account");
        }
    }
}