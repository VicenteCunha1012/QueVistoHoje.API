namespace QueVistoHoje.API.Data.Entities {
    public class Empresa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Morada { get; set; }

        public string Imagem { get; set; }
        public string Nif { get; set; }

        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
