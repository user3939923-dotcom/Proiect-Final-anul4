using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace RestoPro.Data
{
    public class DatabaseHelper
    {
        private static readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["RestoPro"]
            .ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
        public static bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    $"Eroare conexiune BD:\n{ex.Message}",
                    "Eroare",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return false;
            }
        }
    }
}