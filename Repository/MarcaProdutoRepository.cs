using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class MarcaProdutoRepository
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

        public List<MarcaProdutoModel> GetMarcaProduto()
        {
            Connection();
            var ret = new List<MarcaProdutoModel>();
            using(SqlCommand cmd = new SqlCommand("SELECT *             " +
                                                  "FROM dbo.MarcaProduto", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new MarcaProdutoModel()
                    {
                        Id    = (int)    reader["Id"],
                        Nome  = (string) reader["Nome"],
                        Ativo = (bool)   reader["Ativo"]
                    });
                }
                con.Close();
            }
            return ret;
        }

        public MarcaProdutoModel GetMarcaProdutoById(int id)
        {
            Connection();
            MarcaProdutoModel ret = null;
            using (SqlCommand cmd = new SqlCommand("SELECT *              " +
                                                   "FROM dbo.MarcaProduto " +
                                                   "WHERE Id=@Id          ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new MarcaProdutoModel()
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

        public bool PostMarcaProduto(MarcaProdutoModel marcaProdutoModel)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.MarcaProduto(Nome," +
                                                   "                             Ativo)" +
                                                   "                      VALUES(@Nome, " +
                                                   "                            @Ativo)", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value = marcaProdutoModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value = marcaProdutoModel.Ativo;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool PutMarcaProduto(MarcaProdutoModel marcaProdutoModel, int id)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("UPDATE dbo.MarcaProduto " +
                                                   "SET Nome=@Nome,         " +
                                                   "    Ativo=@Ativo        " +
                                                   "WHERE Id=@Id            ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value = marcaProdutoModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value = marcaProdutoModel.Ativo;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool DeleteMarcaProduto(int id)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.MarcaProduto " +
                                                   "WHERE Id=@Id            ", con))
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
