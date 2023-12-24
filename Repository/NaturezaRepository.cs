using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class NaturezaRepository
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

        public List<NaturezaModel> GetNatureza()
        {
            Connection();
            var ret = new List<NaturezaModel>();
            using(SqlCommand cmd = new SqlCommand("SELECT *         " +
                                                  "FROM dbo.Natureza", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    ret.Add(new NaturezaModel()
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

        public NaturezaModel GetNaturezaById(int id)
        {
            Connection();
            NaturezaModel ret = null;
            using(SqlCommand cmd = new SqlCommand("SELECT *          " +
                                                  "FROM dbo.Natureza " +
                                                  "WHERE Id=@Id      ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new NaturezaModel()
                    {
                        Id     = (int)reader["Id"],
                        Codigo = (string)reader["Codigo"],
                        Nome   = (string)reader["Nome"]
                    };
                }
                con.Close();
            }
            return ret;
        }

        public bool PostNatureza(NaturezaModel naturezaModel)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Natureza(Codigo,  " +
                                                  "                         Nome)    " +
                                                  "                  VALUES(@Codigo, " +
                                                  "                         @Nome)   ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = naturezaModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value = naturezaModel.Nome;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool PutNatureza(NaturezaModel naturezaModel, int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("UPDATE dbo.Natureza " +
                                                  "SET Codigo=@Codigo, " +
                                                  "    Nome=@Nome      " +
                                                  "WHERE Id=@Id        ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = naturezaModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = naturezaModel.Nome;
                cmd.Parameters.AddWithValue("@Id",     SqlDbType.Int).Value     = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();

            }
            return ret;
        }

        public bool DeleteNatureza(int id)
        {
            Connection();
            bool ret = false;
            using( SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Natureza " +
                                                   "WHERE Id=@Id             ", con))
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
