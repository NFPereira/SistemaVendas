using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class TipoPessoaRepository
    {
        private static IConfiguration _configuration;

        public static void setConfig(IConfiguration configuration)
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

        public List<TipoPessoaModel> GetTiposPessoa()
        {
            Connection();
            var ret = new List<TipoPessoaModel>();
            using(SqlCommand cmd = new SqlCommand("SELECT *                     " +
                                                  "FROM dbo.TipoPessoa (NOLOCK) ", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new TipoPessoaModel
                    {
                        Id     = (int)    reader["Id"],
                        Codigo = (string) reader["Codigo"],
                        Nome   = (string) reader["Nome"],
                        Ativo  = (bool)   reader["Ativo"]
                    });
                }
                con.Close();
            }
            return ret;
        }

        public TipoPessoaModel GetTipoPessoaById(int id)
        {
            Connection();
            TipoPessoaModel ret = null;
            using (SqlCommand cmd = new SqlCommand("SELECT *                     " +
                                                   "FROM dbo.TipoPessoa (NOLOCK) " +
                                                   "WHERE Id=@Id                 ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new TipoPessoaModel
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

        public bool PostTipoPessoa(TipoPessoaModel tipoPessoaModel)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("INSERT INTO dbo.TipoPessoa (Codigo,  " +
                                                  "                            Nome,    " +
                                                  "                            Ativo)   " +
                                                  "                    VALUES (@Codigo, " +
                                                  "                            @Nome,   " +
                                                  "                            @Ativo)  ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = tipoPessoaModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = tipoPessoaModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo",  SqlDbType.Bit).Value     = tipoPessoaModel.Ativo;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool PutTipoPessoa(TipoPessoaModel tipoPessoaModel, int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("UPDATE dbo.TipoPessoa " +
                                                  "SET Codigo=@Codigo,   " +
                                                  "    Nome=@Nome,       " +
                                                  "    Ativo=@Ativo      " +
                                                  "WHERE Id=@Id          ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = tipoPessoaModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = tipoPessoaModel.Nome;
                cmd.Parameters.AddWithValue("@Ativo",  SqlDbType.Bit).Value     = tipoPessoaModel.Ativo;
                cmd.Parameters.AddWithValue("@Id",      SqlDbType.Int).Value    = id;
                ret = (int)cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return ret;
        }

        public bool DeleteTipoPessoa(int id)
        {
            Connection();
            bool ret = false;
            using(SqlCommand cmd = new SqlCommand("DELETE FROM dbo.TipoPessoa " +
                                                  "WHERE Id=@Id               ", con))
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
