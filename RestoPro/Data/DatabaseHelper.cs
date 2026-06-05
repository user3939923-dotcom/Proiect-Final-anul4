using Microsoft.Data.SqlClient;
using RestoPro.Models;
using System;
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
        public static List<string> GetCategorii()
        {
            var list = new List<string>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT DISTINCT Categorie FROM Produs ORDER BY Categorie",
                    conn);
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(r.GetString(0));
            }
            return list;
        }
        public static List<Produs> FilterProduse(string categorie,
        string search)
        {
            var list = new List<Produs>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT IdProdus, Denumire, Categorie, Pret, " +
                "Disponibil FROM Produs WHERE 1=1";
                if (!string.IsNullOrEmpty(categorie))
                    sql += " AND Categorie = @cat";
                if (!string.IsNullOrEmpty(search))
                    sql += " AND Denumire LIKE @s";

                var cmd = new SqlCommand(sql, conn);
                if (!string.IsNullOrEmpty(categorie))
                    cmd.Parameters.AddWithValue("@cat", categorie);
                if (!string.IsNullOrEmpty(search))
                    cmd.Parameters.AddWithValue("@s", $"%{search}%");

                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(new Produs
                        {
                            IdProdus = r.GetInt32(0),
                            Denumire = r.GetString(1),
                            Categorie = r.GetString(2),
                            Pret = r.GetDecimal(3),
                            Disponibil = r.GetBoolean(4)
                        });
            }
            return list;
        }

        public static void AddProdus(Produs p)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Produs " +
                    "(Denumire, Categorie, Pret, Disponibil) " +
                    "VALUES (@d, @c, @p, @disp)", conn);
                cmd.Parameters.AddWithValue("@d", p.Denumire);
                cmd.Parameters.AddWithValue("@c", p.Categorie);
                cmd.Parameters.AddWithValue("@p", p.Pret);
                cmd.Parameters.AddWithValue("@disp", p.Disponibil);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateProdus(Produs p)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Produs SET Denumire=@d, Categorie=@c, " +
                    "Pret=@p, Disponibil=@disp WHERE IdProdus=@id",
                    conn);
                cmd.Parameters.AddWithValue("@d", p.Denumire);
                cmd.Parameters.AddWithValue("@c", p.Categorie);
                cmd.Parameters.AddWithValue("@p", p.Pret);
                cmd.Parameters.AddWithValue("@disp", p.Disponibil);
                cmd.Parameters.AddWithValue("@id", p.IdProdus);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteProdus(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "DELETE FROM Produs WHERE IdProdus=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static bool CanDeleteProdus(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Comanda WHERE IdProdus=@id",
                    conn);
                cmd.Parameters.AddWithValue("@id", id);
                return (int)cmd.ExecuteScalar() == 0;
            }
        }
        public static List<Comanda> FilterComenzi(int? idMasa,
    string status)
        {
            var list = new List<Comanda>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var sql =
                    "SELECT c.IdComanda, c.IdMasa, c.IdProdus, " +
                    "c.DataComanda, c.Cantitate, c.StatusPlata, " +
                    "m.NumarMasa, p.Denumire, p.Pret " +
                    "FROM Comanda c " +
                    "JOIN Masa m ON c.IdMasa = m.IdMasa " +
                    "JOIN Produs p ON c.IdProdus = p.IdProdus " +
                    "WHERE 1=1";
                if (idMasa.HasValue) sql += " AND c.IdMasa=@m";
                if (status != null) sql += " AND c.StatusPlata=@s";
                sql += " ORDER BY c.DataComanda DESC";

                var cmd = new SqlCommand(sql, conn);
                if (idMasa.HasValue)
                    cmd.Parameters.AddWithValue("@m", idMasa.Value);
                if (status != null)
                    cmd.Parameters.AddWithValue("@s", status);

                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(new Comanda
                        {
                            IdComanda = r.GetInt32(0),
                            IdMasa = r.GetInt32(1),
                            IdProdus = r.GetInt32(2),
                            DataComanda = r.GetDateTime(3),
                            Cantitate = r.GetInt32(4),
                            StatusPlata = r.GetString(5),
                            NumarMasa = r.GetInt32(6).ToString(),
                            DenumireProdus = r.GetString(7),
                            PretProdus = r.GetDecimal(8)
                        });
            }
            return list;
        }

        public static void AddComanda(Comanda c)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Comanda " +
                    "(IdMasa, IdProdus, DataComanda, Cantitate, StatusPlata)" +
                    " VALUES (@m, @p, @d, @c, @s)", conn);
                cmd.Parameters.AddWithValue("@m", c.IdMasa);
                cmd.Parameters.AddWithValue("@p", c.IdProdus);
                cmd.Parameters.AddWithValue("@d", c.DataComanda);
                cmd.Parameters.AddWithValue("@c", c.Cantitate);
                cmd.Parameters.AddWithValue("@s", c.StatusPlata);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteComanda(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "DELETE FROM Comanda WHERE IdComanda=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static bool ComandaExista(int idMasa, int idProdus)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Comanda " +
                    "WHERE IdMasa=@m AND IdProdus=@p", conn);
                cmd.Parameters.AddWithValue("@m", idMasa);
                cmd.Parameters.AddWithValue("@p", idProdus);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public static int? GetMasaIdByNr(int numar)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT IdMasa FROM Masa WHERE NumarMasa=@n", conn);
                cmd.Parameters.AddWithValue("@n", numar);
                var result = cmd.ExecuteScalar();
                return result != null ? (int?)Convert.ToInt32(result) : null;
            }
        }
    }
}