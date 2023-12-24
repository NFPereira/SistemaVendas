using api_vendas.Model;
using System.Data;
using System.Data.SqlClient;

namespace api_vendas.Repository
{
    public class EstadoRepository
    {
        private static IConfiguration _configuration;

        public static void setConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection con;
        public void Connection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            con = new SqlConnection(_configuration.GetConnectionString("stringConexao"));
        }

        public List<EstadoModel> GetEstados()
        {
            Connection();
            var ret = new List<EstadoModel>();
            using (SqlCommand cmd = new SqlCommand("SELECT *                 " +
                                                  "FROM dbo.Estado (NOLOCK) ", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new EstadoModel()
                    {
                        Id = (int)reader["Id"],
                        Codigo = (string)reader["Codigo"],
                        Nome = (string)reader["Nome"],
                        Sigla = (string)reader["Sigla"],
                        IdPais = (int)reader["IdPais"],
                        Ativo = (bool)reader["Ativo"]
                    });
                }
                con.Close();
            }
            return ret;
        }

        public EstadoModel GetEstadoById(int id)
        {
            Connection();
            EstadoModel ret = null;
            using (SqlCommand cmd = new SqlCommand("SELECT *        " +
                                                  "FROM dbo.Estado " +
                                                  "WHERE Id=@Id    ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ret = new EstadoModel()
                    {
                        Id = (int)reader["Id"],
                        Codigo = (string)reader["Codigo"],
                        Nome = (string)reader["Nome"],
                        Sigla = (string)reader["Sigla"],
                        IdPais = (int)reader["IdPais"],
                        Ativo = (bool)reader["Ativo"]
                    };
                }
                con.Close();
            }
            return ret;
        }

        public EstadoModel PostEstado(EstadoModel estadoModel)
        {
            Connection();
            EstadoModel ret = null;
            using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Estado ( Codigo  " +
                                                  "                         ,Nome    " +
                                                  "                         ,Sigla  " +
                                                  "                         ,IdPais  " +
                                                  "                         ,Ativo) " +
                                                  "                VALUES (@Codigo  " +
                                                  "                        ,@Nome    " +
                                                  "                        ,@Sigla   " +
                                                  "                        ,@IdPais  " +
                                                  "                        ,@Ativo)  ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = estadoModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = estadoModel.Nome;
                cmd.Parameters.AddWithValue("@Sigla",  SqlDbType.VarChar).Value = estadoModel.Sigla;
                cmd.Parameters.AddWithValue("@IdPais", SqlDbType.Int).Value     = estadoModel.IdPais;
                cmd.Parameters.AddWithValue("@Ativo",  SqlDbType.Bit).Value     = estadoModel.Ativo;

                if ((int)cmd.ExecuteNonQuery() > 0)
                {
                    ret = new EstadoModel()
                    {
                        Codigo = estadoModel.Codigo,
                        Nome   = estadoModel.Nome,
                        Sigla  = estadoModel.Sigla,
                        IdPais = estadoModel.IdPais,
                        Ativo  = estadoModel.Ativo
                    };
                }

            }
            return ret;
        }

        public bool PutEstado(EstadoModel estadoModel,int id)
        {
            Connection();
            bool ret = false;

            using (SqlCommand cmd = new SqlCommand("UPDATE dbo.Estado  " +
                                                  "SET Codigo=@Codigo, " +
                                                  "    Nome=@Nome,     " +
                                                  "    Sigla=@Sigla,   " +
                                                  "    IdPais=@IdPais  " +
                                                  "WHERE Id=@Id        ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = estadoModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = estadoModel.Nome;
                cmd.Parameters.AddWithValue("@Sigla",  SqlDbType.VarChar).Value = estadoModel.Sigla;
                cmd.Parameters.AddWithValue("@IdPais", SqlDbType.VarChar).Value = estadoModel.IdPais;
                cmd.Parameters.AddWithValue("@Id",     SqlDbType.VarChar).Value = id;

                ret = (int)cmd.ExecuteNonQuery() > 0;

                con.Close();
            }
            return ret;
        }

        public bool DeleteEstado(int id)
        {
            Connection();
            bool ret = false;
            using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Estado " +
                                                  "WHERE Id=@Id           ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;

                ret = (int)cmd.ExecuteNonQuery() > 0;

            }
            return ret;
        }
    }
}
