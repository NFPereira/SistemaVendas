using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class CidadeRepository
    {
        private static IConfiguration _configuration;

        private static void setConfig (IConfiguration configuration)
        {
            configuration = _configuration;
        }

        public SqlConnection con;

        public void Connection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            con = new SqlConnection(_configuration.GetConnectionString("stringConexao"));
        }

        public List<CidadeModel> GetCidades()
        {
            Connection();
            var ret = new List<CidadeModel>();
            using(SqlCommand cmd = new SqlCommand("SELECT *        " +
                                                  "FROM dbo.Cidade ", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new CidadeModel
                    {
                        Id       = (int)reader["Id"],
                        Codigo   = (string)reader["Codigo"],
                        Nome     = (string)reader["Nome"],
                        IdEstado = (int)reader["IdEstado"],
                        Ativo    = (bool)reader["Ativo"]
                    });
                }
                con.Close();
            }
            return ret;
        }

        public CidadeModel GetCidadeById(int id)
        {
            Connection();
            CidadeModel ret = null;
            using (SqlCommand cmd = new SqlCommand("SELECT *         " +
                                                   "FROM dbo.Cidade  " +
                                                   "WHERE Id=@Id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new CidadeModel
                    {
                        Id       = (int)    reader["Id"],
                        Codigo   = (string) reader["Codigo"],
                        Nome     = (string) reader["Nome"],
                        IdEstado = (int)    reader["IdEstado"],
                        Ativo    = (bool)   reader["Ativo"]
                    };
                }
                con.Close();
            }
            return ret;

        }

        public bool PostCidade(CidadeModel cidadeModel)
        {
            Connection();
            bool ret = false;
            using(SqlCommand  cmd = new SqlCommand("INSERT INTO dbo.Cidade (Codigo,    " +
                                                   "                        Nome,      " +
                                                   "                        IdEstado,  " +
                                                   "                        Ativo )    " +
                                                   "                VALUES (@Codigo,   " +
                                                   "                        @Nome,     " +
                                                   "                        @IdEstado, " +
                                                   "                        @Ativo     ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo",   SqlDbType.VarChar).Value = cidadeModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",     SqlDbType.VarChar).Value = cidadeModel.Nome;
                cmd.Parameters.AddWithValue("@IdEstado", SqlDbType.VarChar).Value = cidadeModel.IdEstado;
                cmd.Parameters.AddWithValue("@Ativo",    SqlDbType.Bit).Value     = cidadeModel.Ativo;
                ret = (int) cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool PutCidade(CidadeModel cidadeModel, int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("UPDATE dbo.Cidade  " +
                                                  "SET Codigo=@Codigo,     " +
                                                  "    Nome=@Nome,         " +
                                                  "    IdEstado=@IdEstado, " +
                                                  "    Ativo=@Ativo        " +
                                                  "WHERE Id=@Id            ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo",   SqlDbType.VarChar).Value = cidadeModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",     SqlDbType.VarChar).Value = cidadeModel.Nome;
                cmd.Parameters.AddWithValue("@IdEstado", SqlDbType.VarChar).Value = cidadeModel.IdEstado;
                cmd.Parameters.AddWithValue("@Ativo",    SqlDbType.VarChar).Value = cidadeModel.Ativo;
                cmd.Parameters.AddWithValue("@Id",       SqlDbType.Int).Value     = id;

                ret = (int) cmd.ExecuteNonQuery() > 0;
                con.Close();

            }
            return ret;
        }

        public bool DeleteCidade(int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Cidade " +
                                                  "WHERE Id=@Id           ", con))
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
