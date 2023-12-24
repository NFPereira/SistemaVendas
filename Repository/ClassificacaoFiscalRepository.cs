using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class ClassificacaoFiscalRepository
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

        public List<ClassificacaoFiscalModel> GetClassificacaoFiscal()
        {
            Connection();
            var ret = new List<ClassificacaoFiscalModel>();
            using(SqlCommand cmd = new SqlCommand("SELECT *                     " +
                                                  "FROM dbo.ClassificacaoFiscal ", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new ClassificacaoFiscalModel()
                    {
                        Id    = (int)    reader["Id"],
                        Nome  = (string) reader["Nome"],
                        Ativo = (bool)   reader["Ativo"]
                    });
                }
                con.Close();
            }
            return ret;
        }

        public ClassificacaoFiscalModel GetClassificacaoFiscalById(int id)
        {
            Connection();
            ClassificacaoFiscalModel ret = null;

            using (SqlCommand cmd = new SqlCommand("SELECT *                     " +
                                                   "FROM dbo.ClassificacaoFiscal " +
                                                   "WHERE Id=@Id                 ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new ClassificacaoFiscalModel()
                    {
                        Id    = (int)reader["Id"],
                        Nome  = (string)reader["Nome"],
                        Ativo = (bool)reader["Ativo"]
                    };
                }
                con.Close();
            }
            return ret;

        }

        public bool PostClassificacaoFisccal(ClassificacaoFiscalModel classificacaoFiscalModel)
        {
            Connection();
            bool ret = false;

            using(SqlCommand cmd = new SqlCommand("INSERT INTO dbo.ClassificacaoFiscal(Nome,   " +
                                                  "                                    Ativo)  " +
                                                  "                             VALUES(@Nome,  " +
                                                  "                                    @Ativo) ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome",  SqlDbType.VarChar).Value = classificacaoFiscalModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value     = classificacaoFiscalModel.Ativo;
                ret = (int) cmd.ExecuteNonQuery() > 0;
                con.Close();

            }
            return ret;
        }

        public bool PutClassificacaoFiscal(ClassificacaoFiscalModel classificacaoFiscalModel, int id)
        {
            Connection();
            bool ret = false;

            using(SqlCommand cmd = new SqlCommand("UPDATE dbo.ClassificacaoFiscal " +
                                                  "SET Nome=@Nome,                " +
                                                  "    Ativo=@Ativo               " +
                                                  "WHERE Id=@Id                   ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Nome",  SqlDbType.VarChar).Value = classificacaoFiscalModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value     = classificacaoFiscalModel.Ativo;
                cmd.Parameters.AddWithValue("@Id",    SqlDbType.Int).Value     = id;
                ret = (int) cmd.ExecuteNonQuery() > 0;
                con.Close();

            }
            return ret;
        }

        public bool DeleteClassificacaoFiscal(int id)
        {
            Connection();
            bool ret = false;

            using(SqlCommand cmd = new SqlCommand("DELETE FROM dbo.ClassificacaoFiscal " +
                                                  "WHERE Id=@Id                        ", con))
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
