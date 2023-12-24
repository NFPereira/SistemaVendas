using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class SexoRepository
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

        public List<SexoModel> GetSexos()
        {
            Connection();
            var ret = new List<SexoModel>();
            using(SqlCommand cmd = new SqlCommand("SELECT *              " +
                                                  "FROM dbo.Sexo (NOLOCK)", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    ret.Add(new SexoModel
                    {
                        Id    = (int)    reader["Id"],
                        Nome  = (string) reader["Nome"],
                        Sigla = (string) reader["Sigla"],
                        Ativo = (bool)   reader["Ativo"]
                    });
                }
                con.Close();
            }
            return ret;            
        }

        public SexoModel GetSexoById(int id)
        {
            Connection();
            SexoModel ret = null;
            using (SqlCommand cmd = new SqlCommand("SELECT *               " +
                                                   "FROM dbo.Sexo (NOLOCK) " +
                                                   "WHERE Id=@Id           ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    ret = new SexoModel
                    {
                        Id = (int)reader["Id"],
                        Nome = (string)reader["Nome"],
                        Sigla = (string)reader["Sigla"],
                        Ativo = (bool)reader["Ativo"]
                    };
                }
                con.Close();
            }
            return ret;

        }

        public bool PostSexo(SexoModel sexoModel)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Sexo (Nome,   " +
                                                  "                      Sigla,  " +
                                                  "                      Ativo)  " +
                                                  "              VALUES (@Nome,  " +
                                                  "                      @Sigla, " +
                                                  "                      @Ativo) ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome",  SqlDbType.VarChar).Value = sexoModel.Nome;
                cmd.Parameters.AddWithValue("@Sigla", SqlDbType.VarChar).Value = sexoModel.Sigla;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value     = sexoModel.Ativo;
                con.Close();
            }
            return ret;
        }

        public bool PutSexo(SexoModel sexoModel, int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("UPDATE dbo.Sexo   " +
                                                  "SET Nome=@Nome,   " +
                                                  "    Sigla=@Sigla, " +
                                                  "    Ativo=@Ativo  " +
                                                  "WHERE Id=@Id      ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value = sexoModel.Nome;
                cmd.Parameters.AddWithValue("@Sigla", SqlDbType.VarChar).Value = sexoModel.Sigla;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value = sexoModel.Ativo;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool DeleteSexo(int id)
        {
            Connection();
            bool ret = false;
            using( SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Sexo" +
                                                   "WHERE Id=@Id", con))
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
