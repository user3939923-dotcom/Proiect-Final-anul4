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
        public static void AddMasa(Masa m)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Masa (NumarMasa, Capacitate, Zona) " +
                    "VALUES (@n, @c, @z)", conn);
                cmd.Parameters.AddWithValue("@n", m.NumarMasa);
                cmd.Parameters.AddWithValue("@c", m.Capacitate);
                cmd.Parameters.AddWithValue("@z", m.Zona);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateMasa(Masa m)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Masa SET NumarMasa=@n, Capacitate=@c, " +
                    "Zona=@z WHERE IdMasa=@id", conn);
                cmd.Parameters.AddWithValue("@n", m.NumarMasa);
                cmd.Parameters.AddWithValue("@c", m.Capacitate);
                cmd.Parameters.AddWithValue("@z", m.Zona);
                cmd.Parameters.AddWithValue("@id", m.IdMasa);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteMasa(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "DELETE FROM Masa WHERE IdMasa=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Masa> SearchMese(string search)
        {
            var list = new List<Masa>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT IdMasa, NumarMasa, Capacitate, Zona " +
                    "FROM Masa WHERE Zona LIKE @s OR " +
                    "CAST(NumarMasa AS NVARCHAR) LIKE @s", conn);
                cmd.Parameters.AddWithValue("@s", $"%{search}%");
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        list.Add(new Masa
                        {
                            IdMasa = reader.GetInt32(0),
                            NumarMasa = reader.GetInt32(1),
                            Capacitate = reader.GetInt32(2),
                            Zona = reader.GetString(3)
                        });
            }
            return list;
        }
    }
}