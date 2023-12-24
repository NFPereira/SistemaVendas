using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class FretePorContaRepository
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

        public List<FretePorContaModel> GetFretePorConta()
        {
            Connection();
            var ret = new List<FretePorContaModel>();
            using(SqlCommand cmd = new SqlCommand("SELECT *               " +
                                              "From dbo.FretePorConta ", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new FretePorContaModel()
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

        public FretePorContaModel GetFretePorContaById(int id)
        {
            Connection();
            FretePorContaModel ret = null;
            using(SqlCommand cmd = new SqlCommand("SELECT *               " +
                                                  "FROM dbo.FretePorConta " +
                                                  "WHERE Id=@Id           ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new FretePorContaModel()
                    {
                        Id = (int)reader["Id"],
                        Codigo = (string)reader["Codigo"],
                        Nome = (string)reader["Nome"],
                        Ativo = (bool)reader["Ativo"]
                    };
                }
                con.Close();
            }
            return ret;
        }

        public bool PostFretePorConta(FretePorContaModel fretePorContaModel)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("INSERT INTO dbo.FretePorConta (Codigo,  " +
                                                  "                               Nome,    " +
                                                  "                               Ativo)   " +
                                                  "                       VALUES (@Codigo, " +
                                                  "                               @Nome,   " +
                                                  "                               @Ativo)  ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = fretePorContaModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = fretePorContaModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo",  SqlDbType.Bit).Value     = fretePorContaModel.Ativo;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close() ;
            }
            return ret;
        } 
        
        public bool PutFretePorConta(FretePorContaModel fretePorContaModel, int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("UPDATE dbo.FretePorConta " +
                                                  "SET Codigo=@Codigo,      " +
                                                  "    Nome=@Nome           " +
                                                  "WHERE Id=@Id             ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = fretePorContaModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = fretePorContaModel.Nome;
                cmd.Parameters.AddWithValue("@Id",     SqlDbType.Int).Value     = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }
        
        public bool DeleteFretePorConta(int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("DELETE FROM dbo.FretePorConta " +
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
