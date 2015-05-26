using System.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Dapper
{
    public class SqlConnectionHelper
    {
        public static SqlConnection OpenConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString);
        }
    }
}
