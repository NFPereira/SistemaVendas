﻿namespace api_vendas.Model
{
    public class MovimentoTipoModel
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
