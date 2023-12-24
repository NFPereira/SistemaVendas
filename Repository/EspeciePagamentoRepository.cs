using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class EspeciePagamentoRepository
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

        public List<EspeciePagamentoModel> GetEspeciePagamento()
        {
            Connection();
            var ret = new List<EspeciePagamentoModel>();
            using(SqlCommand cmd = new SqlCommand("SELECT *                 " +
                                                  "FROM dbo.EspeciePagamento", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new EspeciePagamentoModel()
                    {
                        Id     = (int)    reader["Id"],
                        Codigo = (string) reader["Codigo"],
                        Nome   = (string) reader["Nome"]

                    });
                }
                con.Close();
            }
            return ret;
        }

        public EspeciePagamentoModel GetEspeciePagamentoById(int id)
        {
            Connection();
            EspeciePagamentoModel ret = null;
            using(SqlCommand cmd = new SqlCommand("SELECT *                  " +
                                                  "FROM dbo.EspeciePagamento " +
                                                  "WHERE Id=@Id              ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new EspeciePagamentoModel()
                    {
                        Id     = (int)    reader["Id"],
                        Codigo = (string) reader["Codigo"],
                        Nome   = (string) reader["Nome"]
                    };
                }
                con.Close();
            }
            return ret;
        }

        public bool PostEspeciePagamento(EspeciePagamentoModel especiePagamentoModel)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.EspeciePagamento (Codigo, " +
                                                  "                                  Nome )   " +
                                                  "                          VALUES (@Codigo, " +
                                                  "                                  @Nome )  ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = especiePagamentoModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value   = especiePagamentoModel.Nome;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool PutEspeciePagamento(EspeciePagamentoModel especiePagamentoModel, int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("UPDATE dbo.EspeciePagamento " +
                                                  "SET Codigo=@Codigo,         " +
                                                  "    Nome=@Nome              " +
                                                  "WHERE Id=@Id                ", con))
            {
                con.Open();

                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = especiePagamentoModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = especiePagamentoModel.Nome;
                cmd.Parameters.AddWithValue("@Id",     SqlDbType.Int).Value     = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }
        public bool DeleteEspeciePagamento(int id)
        {
            Connection();
            bool ret = false;
            using( SqlCommand cmd = new SqlCommand("DELETE FROM dbo.EspeciePagamento " +
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
