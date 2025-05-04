using System.Data.SqlClient;

namespace BankWebApp.Models
{
    public class BankRepository
    {
        private readonly string _connectionString;

        public BankRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Bank> GetAll()
        {
            var banks = new List<Bank>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("SELECT * FROM Banks", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                banks.Add(new Bank
                {
                    Kod = (int)reader["Kod"],
                    Naim = reader["Naim"].ToString(),
                    Adres = reader["Adres"].ToString(),
                    Tel = reader["Tel"].ToString(),
                    MFO = reader["MFO"].ToString()
                });
            }
            return banks;
        }

        public void Add(Bank bank)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("INSERT INTO Banks (Kod, Naim, Adres, Tel, MFO) VALUES (@Kod, @Naim, @Adres, @Tel, @MFO)", conn);
            cmd.Parameters.AddWithValue("@Kod", bank.Kod);
            cmd.Parameters.AddWithValue("@Naim", bank.Naim);
            cmd.Parameters.AddWithValue("@Adres", bank.Adres);
            cmd.Parameters.AddWithValue("@Tel", bank.Tel);
            cmd.Parameters.AddWithValue("@MFO", bank.MFO);
            cmd.ExecuteNonQuery();
        }

        public List<Bank> Search(string term)
        {
            var result = new List<Bank>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("SELECT * FROM Banks WHERE Naim LIKE @term OR MFO LIKE @term OR Tel LIKE @term", conn);
            cmd.Parameters.AddWithValue("@term", "%" + term + "%");
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new Bank
                {
                    Kod = (int)reader["Kod"],
                    Naim = reader["Naim"].ToString(),
                    Adres = reader["Adres"].ToString(),
                    Tel = reader["Tel"].ToString(),
                    MFO = reader["MFO"].ToString()
                });
            }

            return result;
        }

        public void Delete(int kod)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("DELETE FROM Banks WHERE Kod = @Kod", conn);
            cmd.Parameters.AddWithValue("@Kod", kod);
            cmd.ExecuteNonQuery();
        }
    }
}