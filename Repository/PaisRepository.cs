using api_vendas.Model;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;

namespace api_vendas.Repository
{
    public class PaisRepository
    {
        private static IConfiguration _configuration;

        public static void SetConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection con;

        public void conection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            con = new SqlConnection(_configuration.GetConnectionString("stringConexao"));
        }

        public List<PaisModel> GetPais()
        {
            conection();
            var ret = new List<PaisModel>();

            using (SqlCommand cmd = new SqlCommand("SELECT *              " +
                                                   "FROM dbo.Pais (NOLOCK)", con))
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret.Add(new PaisModel()
                    {
                        Id     = (int)    reader["Id"]
                       ,Codigo = (string) reader["Codigo"]
                       ,Nome   = (string) reader["Nome"]
                       ,Sigla  = (string) reader["Sigla"]
                       ,Ativo  = (bool)   reader["Ativo"]
                    });

                }
                con.Close();
            }
            return ret;
        }

        public PaisModel GetPaisById(int id)
        {
            conection();

            PaisModel ret = null;

            using(SqlCommand cmd = new SqlCommand("SELECT *               " +
                                                  "FROM dbo.Pais (NOLOCK) " +
                                                  "WHERE Id=@Id           ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("Id", SqlDbType.Int).Value = id;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ret = new PaisModel()
                    {
                        Id     = (int)    reader["Id"]
                       ,Codigo = (string) reader["Codigo"]
                       ,Nome   = (string) reader["Nome"]
                       ,Sigla  = (string) reader["Sigla"]
                       ,Ativo  = (bool)   reader["Ativo"]
                    };
                }              
            }
            return ret;
        }

        public PaisModel PostPais(PaisModel paisModel)
        {
            conection();
            PaisModel ret = null;

            using(SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Pais ( Codigo  " +
                                                  "                       ,Nome   " +
                                                  "                       ,Sigla  " +
                                                  "                       ,Ativo  " +
                                                  "                     )         " +
                                                  "              VALUES ( @Codigo " +
                                                  "                      ,@Nome   " +
                                                  "                      ,@Sigla  " +
                                                  "                      ,@Ativo  " +
                                                  "                     )         ", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = paisModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = paisModel.Nome;
                cmd.Parameters.AddWithValue("@Sigla",  SqlDbType.VarChar).Value = paisModel.Sigla;
                cmd.Parameters.AddWithValue("@Ativo",  SqlDbType.VarChar).Value = paisModel.Ativo;

                if((int)cmd.ExecuteNonQuery()>0){
                    ret = new PaisModel()
                    {
                        Id     = paisModel.Id
                       ,Codigo = paisModel.Codigo
                       ,Nome   = paisModel.Nome
                       ,Sigla  = paisModel.Sigla
                       ,Ativo  = paisModel.Ativo
                    };
                }

            }
            return ret;
        }

        public bool PutPais(PaisModel paisModel, int id)
        {
            conection();
            bool ret = false;

            using(SqlCommand cmd = new SqlCommand("UPDATE dbo.Pais     " +
                                                  "SET Codigo=@Codigo  " +
                                                  "   ,Nome=@Nome      " +
                                                  "   ,Sigla=@Sigla    " +
                                                  "   ,Ativo=@Ativo    " +
                                                  "WHERE Id=@Id        ", con))
            {
                con.Open();

                cmd.Parameters.AddWithValue("@Codigo", SqlDbType.VarChar).Value = paisModel.Codigo;
                cmd.Parameters.AddWithValue("@Nome",   SqlDbType.VarChar).Value = paisModel.Nome;
                cmd.Parameters.AddWithValue("@Sigla",  SqlDbType.VarChar).Value = paisModel.Sigla;
                cmd.Parameters.AddWithValue("@Ativo",  SqlDbType.Bit).Value     = paisModel.Ativo;
                cmd.Parameters.AddWithValue("@Id",     SqlDbType.Int).Value     = id;

                ret = (int) cmd.ExecuteNonQuery() > 0;

                con.Close();
            }
            return ret;
        }

        public bool DeletePais(int id)
        {
            conection();
            var ret = false;
            using(SqlCommand cmd = new SqlCommand("DELETE dbo.Pais " +
                                                  "WHERE Id=@Id    ", con))
            {
                con.Open();

                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;

                ret = (int) cmd.ExecuteNonQuery() > 0;

                con.Close();
            }
            return ret;
        }



    }
}
