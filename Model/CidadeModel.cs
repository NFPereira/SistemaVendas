namespace api_vendas.Model
{
    public class CidadeModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }

        public int IdEstado { get; set; }

        public bool Ativo { get; set; }
    }
}
