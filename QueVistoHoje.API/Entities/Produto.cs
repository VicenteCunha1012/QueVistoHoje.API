﻿// Produto.cs
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueVistoHoje.API.Entities {
    public class Produto {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Stock { get; set; }
        public string Imagem { get; set; }
        public bool Promocao { get; set; }
        public string Estado { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public List<Categoria> Categoria { get; set; } = new();
    }
}
