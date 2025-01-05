// Encomenda.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace QueVistoHoje.API.Data.Entities {
    public class Encomenda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MetodoPagamento MetodoPagamento { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EncomendaState Estado { get; set; }
        public string EnderecoEntrega { get; set; }
        public decimal PrecoTotal { get; set; }

        public List<EncomendaProduto> EncomendaProdutos { get; set; } = new();

        public Encomenda()
        {
            EncomendaProdutos = new List<EncomendaProduto>();
            Estado = EncomendaState.POR_ENTREGAR;
            MetodoPagamento = MetodoPagamento.CARTAO_CREDITO_OU_DEBITO;

        }

        public void CalcularPrecoTotal()
        {
            Console.WriteLine("A calcular de " + EncomendaProdutos.Count + " produtos");
            PrecoTotal = EncomendaProdutos.Sum(ep => ep.Produto.Preco * ep.Quantidade);
        }

        public static string GetEstadoString(EncomendaState estado)
        {
            return estado switch
            {
                EncomendaState.ENTREGUE => "Entregue",
                EncomendaState.POR_ENTREGAR => "Por Entregar",
                EncomendaState.CANCELADA => "Cancelada",
                _ => "Desconhecido"
            };
        }

        public static string GetMetodoPagamentoString(MetodoPagamento metodoPagamento)
        {
            return metodoPagamento switch
            {
                MetodoPagamento.CARTAO_CREDITO_OU_DEBITO => "Cartão de Crédito ou Débito",
                MetodoPagamento.TRANSFERENCA_BANCARIA => "Transferência Bancária",
                MetodoPagamento.MB_WAY => "MB Way",
                _ => "Desconhecido"
            };
        }
    }
    public enum EncomendaState
    {
        POR_ENTREGAR,
        ENTREGUE,
        CANCELADA
    }
    public enum MetodoPagamento
    {
        CARTAO_CREDITO_OU_DEBITO,
        TRANSFERENCA_BANCARIA,
        MB_WAY
    }
}