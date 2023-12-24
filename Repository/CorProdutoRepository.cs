using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class CorProdutoRepository
    {
        private static IConfiguration _configuration;

        private static void setConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private SqlConnection con;

        public void Connection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            con = new SqlConnection(_configuration.GetConnectionString("stringConexao"));
        }

        public List<CorProdutoModel> GetCorProduto()
        {
            Connection();

            var ret = new List<CorProdutoModel>();
            using (SqlCommand cmd = new SqlCommand("SELECT Id,                 " +
                                                  "       Nome,                " +
                                                  "       Ativo                " +
                                                  "FROM dbo.CorProduto (NOLOCK)", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new CorProdutoModel()
                    {
                        Id    = (int)reader["Id"],
                        Nome  = (string)reader["Nome"],
                        Ativo = (bool)reader["Ativo"]

                    });
                }
                con.Close();
            }
            return ret;
        }

        public CorProdutoModel GetCorProdutoById(int id)
        {
            Connection();

            CorProdutoModel ret = null;
            using (SqlCommand cmd = new SqlCommand("SELECT Id,                  " +
                                                   "       Nome,                " +
                                                   "       Ativo                " +
                                                   "FROM dbo.CorProduto (NOLOCK)" +
                                                   "WHERE Id=@Id                ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new CorProdutoModel()
                    {
                        Id    = (int)reader["Id"],
                        Nome  = (string)reader["Nome"],
                        Ativo = (bool)reader["Ativo"]

                    };
                }
                con.Close();
            }
            return ret;
        }

        public bool PostCorProduto(CorProdutoModel corProdutoModel)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.CorProduto (Nome,  " +
                                                  "                            Ativo) " +
                                                  "                    VALUES (@Nome, " +
                                                  "                            @Ativo)", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome",  SqlDbType.VarChar).Value = corProdutoModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value     = corProdutoModel.Ativo;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool PutCorProduto(CorProdutoModel corProdutoModel, int id)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("UPDATE dbo.CorProduto " +
                                                   "SET Nome=@Nome,       " +
                                                   "    Ativo=@Ativo      " +
                                                   "WHERE Id=@Id          ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome",  SqlDbType.VarChar).Value = corProdutoModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value     = corProdutoModel.Ativo;
                cmd.Parameters.AddWithValue("@Id",    SqlDbType.Int).Value     = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();

            }
            return ret;
        }

        public bool DeleteCorProduto(int id)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.CorProduto " +
                                                   "WHERE Id=@Id               ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
            }
            return ret;
        }
    }
}
