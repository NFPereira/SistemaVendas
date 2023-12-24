namespace api_vendas.Model
{
    public class EstadoModel
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nome { get; set; }
        public string? Sigla { get; set; }
        public int IdPais { get; set; }
        public bool Ativo { get; set; }
    }
}
