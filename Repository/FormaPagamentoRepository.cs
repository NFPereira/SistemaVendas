using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class FormaPagamentoRepository
    {
        private static IConfiguration _configuration;

        private static void setConfig(IConfiguration configuration) { 
            _configuration = configuration;
        }
        private SqlConnection con;

        public void Connection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            con = new SqlConnection(_configuration.GetConnectionString("stringConexao"));
        }

        public List<FormaPagamentoModel> GetFormaPagamento()
        {
            Connection();
            var ret = new List<FormaPagamentoModel>();
            using(SqlCommand cmd = new SqlCommand("SELECT *                " +
                                                  "FROM dbo.FormaPagamento ", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new FormaPagamentoModel()
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

        public FormaPagamentoModel GetFormaPagamentoById(int id)
        {
            Connection();
            FormaPagamentoModel ret = null;
            using (SqlCommand cmd = new SqlCommand("SELECT *                " +
                                                   "FROM dbo.FormaPagamento " +
                                                   "WHERE Id=@Id            ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    ret = new FormaPagamentoModel()
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
        public bool PostFormaPagamento(FormaPagamentoModel formaPagamentoModel)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("INSERT INTO dbo.FormaPagamento(Codigo,  " +
                                                  "                               Nome)    " +
                                                  "                        VALUES(@Codigo, " +
                                                  "                               @Nome)   ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = formaPagamentoModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = formaPagamentoModel.Nome;
                ret = (int) cmd.ExecuteNonQuery() > 0;
                con.Close();

            }
            return ret;
        }

        public bool PutFormaPagamento(FormaPagamentoModel formaPagamentoModel, int id)
        {
            Connection();
            bool ret = false;
            using( SqlCommand cmd = new SqlCommand("UPDATE dbo.FormaPagamento " +
                                                   "SET Codigo=@Codigo,       " +
                                                   "    Nome=@Nome            " +
                                                   "WHERE Id=@Id              ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = formaPagamentoModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = formaPagamentoModel.Nome;
                cmd.Parameters.AddWithValue("@Id",     SqlDbType.Int).Value     = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();

            }
            return ret;
        }

        public bool DeleteFormaPagamento(int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("DELETE FROM dbo.FormaPagamento " +
                                                  "WHERE Id=@Id                   ", con))
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
