namespace QueVistoHoje.API.Entities
{
    public class Transportadora
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        //Referencias
        public List<Encomenda> Encomendas { get; set; } = new List<Encomenda>();
        //encomendas à qual esta transportadora está assigned

    }
}
