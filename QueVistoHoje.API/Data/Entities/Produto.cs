// Produto.cs
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueVistoHoje.API.Data.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Stock { get; set; }
        public string Imagem { get; set; }
        public string Estado { get; set; }

        public int EmpresaId { get; set; } // Chave estrangeira
        public Empresa Empresa { get; set; } // Propriedade de Navegação


        public List<Categoria> Categoria { get; set; } = new(); // Inicializamos para evitar null reference
    }
}