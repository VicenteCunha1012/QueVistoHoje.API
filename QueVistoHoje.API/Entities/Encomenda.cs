using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueVistoHoje.API.Data;

namespace QueVistoHoje.API.Entities
{
    public class Encomenda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Estado { get; set; }
        public string EnderecoEntrega { get; set; }
        public ApplicationUser Cliente { get; set; }
        public List<Produto> Produtos { get; set; }

        public Transportadora Transportadora { get; set; }

        public Encomenda()
        {
            Produtos = new List<Produto>();
        }
    }
}
