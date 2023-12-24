using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class NivelUsuarioRepository
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

        public List<NivelUsuarioModel> GetNivelUsuario()
        {
            Connection();
            var ret = new List<NivelUsuarioModel>();
            using(SqlCommand cmd = new SqlCommand("SELECT *              " +
                                                  "FROM dbo.NivelUsuario ", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    ret.Add(new NivelUsuarioModel()
                    {
                        Id = (int)reader["Id"],
                        Codigo = (string)reader["Codigo"],
                        Nome = (string)reader["Nome"],
                        Ativo = (bool)reader["Ativo"]
                    });
                }
                con.Close();
            }
            return ret;
        }

        public NivelUsuarioModel GetNivelUsuarioById(int id)
        {
            Connection();
            NivelUsuarioModel ret = null;
            using(SqlCommand cmd = new SqlCommand("SELECT *              " +
                                                  "From dbo.NivelUsuario " +
                                                  "WHERE Id=@Id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new NivelUsuarioModel()
                    {
                        Id     = (int)    reader["Id"],
                        Codigo = (string) reader["Codigo"],
                        Nome   = (string) reader["Nome"],
                        Ativo  = (bool)   reader["Ativo"]
                    };
                }
                con.Close();
            }
            return ret;
        }

        public bool PostNivelUsuario(NivelUsuarioModel nivelUsuarioModel)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("INSERT INTO dbo.NivelUsuario ( Codigo,   " +
                                                  "                               Nome,     " +
                                                  "                               Ativo )   " +
                                                  "                       VALUES (@Codigo,  " +
                                                  "                               @Nome,    " +
                                                  "                               @Ativo)   ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = nivelUsuarioModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = nivelUsuarioModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo",  SqlDbType.Bit).Value     = nivelUsuarioModel.Ativo;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool PutNivelUsuario(NivelUsuarioModel nivelUsuarioModel, int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("UPDATE dbo.NivelUsuario " +
                                                  "SET Codigo=@Codigo,     " +
                                                  "    Nome=@Nome,         " +
                                                  "    Ativo=@Ativo        " +
                                                  "WHERE Id=@Id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = nivelUsuarioModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = nivelUsuarioModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo",  SqlDbType.Bit).Value     = nivelUsuarioModel.Ativo;
                cmd.Parameters.AddWithValue("@Id",     SqlDbType.Int).Value     = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();

            }
            return ret;
        }

        public bool DeleteNivelUsuario(int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("DELETE FROM dbo.NivelUsuario " +
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
