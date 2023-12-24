using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class MovimentoTipoRepository
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

        public List<MovimentoTipoModel> GetMovimentoTipo()
        {
            Connection();
            var ret = new List<MovimentoTipoModel>();
            using(SqlCommand cmd = new SqlCommand("SELECT *              " +
                                                  "FROM dbo.MovimentoTipo", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new MovimentoTipoModel()
                    {
                        Id     = (int)    reader["Id"],
                        Codigo = (string) reader["Codigo"],
                        Nome   = (string) reader["Nome"],
                        Ativo  = (bool)   reader["Ativo"]
                    });
                }
                con.Close();
            }
            return ret;
        }

        public MovimentoTipoModel GetMovimentoTipoById(int id)
        {
            Connection();
            MovimentoTipoModel ret = null;
            using (SqlCommand cmd = new SqlCommand("SELECT *               " +
                                                   "FROM dbo.MovimentoTipo " +
                                                   "WHERE Id=@Id           ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new MovimentoTipoModel()
                    {
                        Id     = (int)reader["Id"],
                        Codigo = (string)reader["Codigo"],
                        Nome   = (string)reader["Nome"],
                        Ativo  = (bool)reader["Ativo"]
                    };
                }
                con.Close();
            }
            return ret;
        }

        public bool PostMovimentoTipo(MovimentoTipoModel movimentoTipoModel)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.MovimentoTipo(Codigo,  " +
                                                   "                              Nome,    " +
                                                   "                              Ativo )  " +
                                                   "                       VALUES(@Codigo, " +
                                                   "                              @Nome,   " +
                                                   "                              @Ativo)  ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = movimentoTipoModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = movimentoTipoModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo",  SqlDbType.Bit).Value     = movimentoTipoModel.Ativo;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool PutMovimentoTipo(MovimentoTipoModel movimentoTipoModel, int id)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("UPDATE dbo.MovimentoTipo " +
                                                   "SET Codigo=@Codigo,      " +
                                                   "    Nome=@Nome,          " +
                                                   "    Ativo=@Ativo         " +
                                                   "WHERE Id=@Id             ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = movimentoTipoModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = movimentoTipoModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo",  SqlDbType.Bit).Value     = movimentoTipoModel.Ativo;
                cmd.Parameters.AddWithValue("@Id",     SqlDbType.Int).Value     = id;
                ret = (int) cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool DeleteMovimentoTipo(int id)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.MovimentoTipo " +
                                                   "WHERE Id=@Id ", con))
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
