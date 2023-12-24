using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class TipoMovimentoRepository
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

        public List<TipoMovimentoModel> GetTipoMovimento()
        {
            Connection();
            var ret = new List<TipoMovimentoModel>();
            using (SqlCommand cmd = new SqlCommand("SELECT *              " +
                                                  "FROM dbo.TipoMovimento", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new TipoMovimentoModel()
                    {
                        Id = (int)reader["Id"],
                        Codigo = (string)reader["Codigo"],
                        Nome = (string)reader["Nome"]
                    });
                }
                con.Close();
            }
            return ret;
        }

        public TipoMovimentoModel GetTipoMovimentoById(int id)
        {
            Connection();
            TipoMovimentoModel ret = null;
            using (SqlCommand cmd = new SqlCommand("SELECT *               " +
                                                   "FROM dbo.TipoMovimento " +
                                                   "WHERE Id=@Id           ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret = new TipoMovimentoModel()
                    {
                        Id = (int)reader["Id"],
                        Codigo = (string)reader["Codigo"],
                        Nome = (string)reader["Nome"]
                    };
                }
                con.Close();
            }
            return ret;
        }

        public bool PostTipoMovimento(TipoMovimentoModel tipoMovimentoModel)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.TipoMovimento (Codigo,  " +
                                                   "                               Nome )   " +
                                                   "                        VALUES(@Codigo, " +
                                                   "                               @Nome )  ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = tipoMovimentoModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value = tipoMovimentoModel.Nome;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool PutTipoMovimento(TipoMovimentoModel tipoMovimentoModel, int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("UPDATE dbo.TipoMovimento " +
                                                  "SET Codigo=@Codigo,      " +
                                                  "    Nome=@Nome           " +
                                                  "WHERE Id=@Id             ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = tipoMovimentoModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value = tipoMovimentoModel.Nome;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool DeleteTipoMovimento(int id)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.TipoMovimento " +
                                                   "WHERE Id=@Id                  ", con))
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
