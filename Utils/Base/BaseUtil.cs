using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using mHealthProject.Interfaces;
using mHealthProject.Services;

namespace mHealthProject.Utils.Base;

public partial class BaseUtil
{
    private readonly ImHealthDB _db = new MHealthDBService();

    #region Protected Methods

    protected SqlConnection connection()
    {
        return _db.GetConnection();
    }

    protected SqlCommand command(string sql, SqlConnection connection)
    {
        return _db.GetCommand(sql, connection);
    }
    
    protected void Log(string msg)
    {
        Console.WriteLine(msg);
    }

    protected void LogError(Exception ex, string util)
    {
        Console.WriteLine($" Error in {util} {ex.Message} - {ex.StackTrace}");
    }
    
    protected string createDisplayName(string fname, string lname)
    {
        return string.Concat(fname, " ", lname[..1], ".");
    }

    protected string encryptPassword(string pwd)
    {
        return HashStringWithSHA512(pwd);
    }
    
    #endregion


    #region Private Methods

    [GeneratedRegex(@"[^\d]")]
    private static partial Regex MyRegex();
    
    private static string HashStringWithSHA512(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);
        
        var hashBytes = SHA512.HashData(inputBytes);
        
        var sb = new StringBuilder();
        
        foreach (var b in hashBytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }

    #endregion
    
}