using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueVistoHoje.API.Entities
{
    public class Pagamento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string MetodoPagamento { get; set; }

        //Relacionamento
        public Encomenda Encomenda { get; set; }
    }
}
