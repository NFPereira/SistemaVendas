using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class CategoriaProdutoRepository
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

        public List<CategoriaProdutoModel> GetCategoriasProdutos()
        {
            Connection();
            var ret = new List<CategoriaProdutoModel>();

            using(SqlCommand cmd = new SqlCommand("SELECT *                  " +
                                                  "FROM dbo.CategoriaProduto ", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new CategoriaProdutoModel()
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
        
        public CategoriaProdutoModel GetCategoriasProdutoById(int id)
        {
            Connection();
            CategoriaProdutoModel ret = null;

            using (SqlCommand cmd = new SqlCommand("SELECT *                  " +
                                                   "FROM dbo.CategoriaProduto " +
                                                   "WHERE Id=@Id              ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new CategoriaProdutoModel()
                    {
                        Id    = (int)    reader["Id"],
                        Nome  = (string) reader["Nome"],
                        Ativo = (bool)   reader["Ativo"]
                    };
                }
                con.Close();
            }
            return ret;
        }

        public bool PostCategoriasProduto(CategoriaProdutoModel categoriaProdutoModel)
        {
            Connection();
            bool ret = false;

            using(SqlCommand cmd = new SqlCommand("INSERT INTO dbo.CategoriaProduto (Nome,    " +
                                                  "                                  Ativo )  " +
                                                  "                           VALUES(@Nome,   " +
                                                  "                                  @Ativo ) ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome",  SqlDbType.VarChar).Value = categoriaProdutoModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value     = categoriaProdutoModel.Ativo;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }
        
        public bool PutCategoriasProduto(CategoriaProdutoModel categoriaProdutoModel, int id)
        {
            Connection();
            bool ret = false;

            using (SqlCommand cmd = new SqlCommand("UPDATE dbo.CategoriaProduto " +
                                                   "SET Nome=@Nome,             " +
                                                   "    Ativo=@Ativo            " +
                                                   "WHERE Id=@Id                ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome",  SqlDbType.VarChar).Value = categoriaProdutoModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value = categoriaProdutoModel.Ativo;
                cmd.Parameters.AddWithValue("@Id",    SqlDbType.Int).Value = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }
        
        public bool DeleteCategoriasProduto(int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("DELETE FROM dbo.CategoriaProduto " +
                                                  "WHERE Id=@Id                     ", con))
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
