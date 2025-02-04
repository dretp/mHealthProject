using System.Text;
using Azure.Core;
using mHealthProject.Models.Auth;
using mHealthProject.Models.User;
using mHealthProject.Utils.Base;

namespace mHealthProject.Utils.Auth;

public class AuthUtil : BaseUtil
{

    public AuthUtil()
    {
        
    }

    public async Task<BaseUser> PerformAuth(AuthRequest authRequest)
    {
        var sql = new StringBuilder("SELECT id, user_type_id AS uType, username, first_name, last_name, password");
        sql.Append(" FROM [user] WHERE username = @userName;");


        try
        {
            await using var conn = connection();
            var cmd = command(sql.ToString(), conn);

            cmd.Parameters.AddWithValue("userName", authRequest.Username);
            
            await using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                throw new Exception("Username or password is incorrect");
            }

            var baseUser = new BaseUser();

            var pwd = string.Empty;
            
            // retrieve details to  create base user for auth token
            while(await reader.ReadAsync())
            {
                baseUser.Id = reader.GetInt32(reader.GetOrdinal("id"));
                baseUser.Username = reader.GetString(reader.GetOrdinal("username"));
                baseUser.FirstName = reader.GetString(reader.GetOrdinal("first_name"));
                baseUser.LastName = reader.GetString(reader.GetOrdinal("last_name"));
                baseUser.UserType = (UserTypeEnum)reader.GetInt32(reader.GetOrdinal("uType"));
                pwd = reader.GetString(reader.GetOrdinal("password"));
            }

            // check passwords to see if they match
            if (!await doesPasswordMatch(authRequest.Password, pwd))
            {
                throw new Exception("Password is incorrect");
            }
            
            return baseUser;
        }
        catch (Exception e)
        {
            LogError(e, "AuthService.PerformAuth");
            throw new Exception("Invalid username or password");
        }
    }
    
    
    private async Task<bool> doesPasswordMatch(string enteredPwd, string storedPwd)
    {
        var comparePwd = encryptPassword(enteredPwd);
        return (comparePwd.Equals(storedPwd, StringComparison.OrdinalIgnoreCase));
    }
}