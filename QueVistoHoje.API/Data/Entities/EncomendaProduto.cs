// EncomendaProduto.cs
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QueVistoHoje.API.Data.Entities
{
    public class EncomendaProduto
    {
        [Key]
        public int Id { get; set; }

        public int EncomendaId { get; set; }
        [ForeignKey("EncomendaId")]
        public Encomenda Encomenda { get; set; }

        public int ProdutoId { get; set; }
        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }
    }
}