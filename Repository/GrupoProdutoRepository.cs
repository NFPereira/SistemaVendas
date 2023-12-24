using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class GrupoProdutoRepository
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
        public List<GrupoProdutoModel> GetGrupoProduto() {
            Connection();
            var ret = new List<GrupoProdutoModel>();
            using(SqlCommand cmd = new SqlCommand("SELECT *             " +
                                                  "FROM dbo.GrupoProduto", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new GrupoProdutoModel()
                    {
                        Id = (int)reader["Id"],
                        Nome = (string)reader["Nome"],
                        Ativo = (bool)reader["Ativo"]
                    });
                }
                con.Close();
            }
            return ret;
        }

        public GrupoProdutoModel GetGrupoProdutoById(int id)
        {
            Connection();
            GrupoProdutoModel ret = null;
            using (SqlCommand cmd = new SqlCommand("SELECT *             " +
                                                  "FROM dbo.GrupoProduto " +
                                                  "WHERE Id=@Id          ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new GrupoProdutoModel()
                    {
                        Id = (int)reader["Id"],
                        Nome = (string)reader["Nome"],
                        Ativo = (bool)reader["Ativo"]
                    };
                }
                con.Close();
            }
            return ret;

        }
        public bool PostGrupoProduto(GrupoProdutoModel grupoProdutoModel)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("INSERT INTO dbo.GrupoProduto(Nome,  " +
                                                  "                             Ativo) " +
                                                  "                      VALUES(@Nome, " +
                                                  "                             @Ativo)", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value = grupoProdutoModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value = grupoProdutoModel.Ativo;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }
        public bool PutGrupoProduto(GrupoProdutoModel grupoProdutoModel, int id)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("UPDATE dbo.GrupoProduto " +
                                                  " SET Nome=@Nome,         " +
                                                  "     Ativo=@Ativo        " +
                                                  "WHERE Id=@Id             ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value = grupoProdutoModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value = grupoProdutoModel.Ativo;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool DeleteGrupoProduto(int id)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.GrupoProduto " +                                                  
                                                   "WHERE Id=@Id                 ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }
    }
}
