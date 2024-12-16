// Categoria.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueVistoHoje.API.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public int? CategoriaPaiId { get; set; }
        public string Nome { get; set; }

        public Categoria? CategoriaPai { get; set; }
        public List<Categoria> Subcategorias { get; set; }
        public List<Produto> Produtos { get; set; } = new(); // Inicializamos para evitar null reference

        public Categoria()
        {
            Subcategorias = new List<Categoria>();
        }
    }
}