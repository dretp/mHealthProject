using Microsoft.Data.SqlClient;

namespace mHealthProject.Interfaces;

public interface ImHealthDB
{
    SqlConnection GetConnection();
    
    SqlCommand GetCommand(string SqlStatement, SqlConnection connection);
}