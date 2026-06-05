using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

namespace RestoPro.Data
{
    public class DatabaseHelper
    {
        private static string _connectionString =
            ConfigurationManager.ConnectionStrings["RestoPro"]
            .ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}