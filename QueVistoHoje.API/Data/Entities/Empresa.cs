using System.ComponentModel.DataAnnotations;

namespace QueVistoHoje.API.Data.Entities {
    public class Empresa {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Nif { get; set; }
        [Required]
        public string Morada { get; set; }
        [Required]
        public string Imagem { get; set; }

        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
