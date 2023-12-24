using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class LocalArmazenamentoRepository
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

        public List<LocalArmazenamentoModel> GetLocalArmazenamento()
        {
            Connection();
            var ret = new List<LocalArmazenamentoModel>();
            using(SqlCommand cmd = new SqlCommand("SELECT *                   " +
                                                  "FROM dbo.LocalArmazenamento", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    ret.Add(new LocalArmazenamentoModel()
                    {
                        Id = (int)reader["Id"],
                        Nome = (string)reader["Nome"],
                        Ativo = (bool)reader["Ativo"]
                    });
                }
                con.Close();
            }
            return ret;
        }

        public LocalArmazenamentoModel GetLocalArmazenamentoById(int id)
        {
            Connection();
            LocalArmazenamentoModel ret = null;
            using (SqlCommand cmd = new SqlCommand("SELECT *                    " +
                                                   "FROM dbo.LocalArmazenamento " +
                                                   "WHERE Id=@Id                ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new LocalArmazenamentoModel()
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

        public bool PostLocalArmazenamento(LocalArmazenamentoModel localArmazenamentoModel)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.LocalArmazenamento(Nome,   " +
                                                   "                                   Ativo)  " +
                                                   "                            VALUES(@Nome,  " +
                                                   "                                   @Ativo) ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome",  SqlDbType.VarChar).Value = localArmazenamentoModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value     = localArmazenamentoModel.Ativo;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }


        public bool PutLocalArmazenamento(LocalArmazenamentoModel localArmazenamentoModel, int id)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("UPDATE dbo.LocalArmazenamento " +
                                                   "SET Nome=@Nome,               " +
                                                   "    Ativo=@Ativo              " +
                                                   "WHERE Id=@Id                  ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome",  SqlDbType.VarChar).Value = localArmazenamentoModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value     = localArmazenamentoModel.Ativo;
                cmd.Parameters.AddWithValue("@Id",    SqlDbType.Int).Value     = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool DeleteLocalArmazenamento(int id)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.LocalArmazenamento " +
                                                   "WHERE Id=@Id                       ", con))
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
