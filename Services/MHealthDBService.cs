using Microsoft.Data.SqlClient;
using mHealthProject.Interfaces;
using mHealthProject.Models.Configuration;

namespace mHealthProject.Services;

public class MHealthDBService : ImHealthDB
{
    private MHealthConfiguration? _configuration { get; } = MHealthConfiguration.Instance;
    
    public SqlConnection GetConnection()
    {
        var connection =  new SqlConnection(_configuration.ConnectionString);
        connection.Open();
        return connection;
    }

    public SqlCommand GetCommand(string SqlStatement, SqlConnection connection)
    {
        return new SqlCommand(SqlStatement, connection);
    }
}