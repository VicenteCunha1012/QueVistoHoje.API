using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace QueVistoHoje.API.Data.Entities {

    public enum EncomendaState {
        ENTREGUE,
        POR_ENTREGAR,
        CANCELADA
    }
    public enum MetodoPagamento {
        CARTAO_CREDITO_OU_DEBITO,
        TRANSFERENCA_BANCARIA,
        MB_WAY
        // devem haver mais...
    }
    public class Encomenda {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string EnderecoEntrega { get; set; }
        public ApplicationUser Cliente { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MetodoPagamento MetodoPagamento { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EncomendaState Estado { get; set; }

        public List<EncomendaProduto> EncomendaProdutos { get; set; } = new(); // New list to handle the relationship
        public Encomenda() {
            EncomendaProdutos = new List<EncomendaProduto>(); // Initialize the list
        }
    }
}