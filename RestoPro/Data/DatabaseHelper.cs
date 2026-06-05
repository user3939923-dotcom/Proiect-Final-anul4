using Microsoft.Data.SqlClient;
using RestoPro.Models;
using System.Collections.Generic;
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
        public static List<Masa> GetAllMese()
        {
            var list = new List<Masa>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT IdMasa, NumarMasa, Capacitate, Zona FROM Masa",
                    conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Masa
                        {
                            IdMasa = reader.GetInt32(0),
                            NumarMasa = reader.GetInt32(1),
                            Capacitate = reader.GetInt32(2),
                            Zona = reader.GetString(3)
                        });
                    }
                }
            }
            return list;
        }

        public static List<Produs> GetAllProduse()
        {
            var list = new List<Produs>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT IdProdus, Denumire, Categorie, Pret, " +
                    "Disponibil FROM Produs", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Produs
                        {
                            IdProdus = reader.GetInt32(0),
                            Denumire = reader.GetString(1),
                            Categorie = reader.GetString(2),
                            Pret = reader.GetDecimal(3),
                            Disponibil = reader.GetBoolean(4)
                        });
                    }
                }
            }
            return list;
        }
    }
}