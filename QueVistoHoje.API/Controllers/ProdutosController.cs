using Microsoft.AspNetCore.Mvc;
using QueVistoHoje.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueVistoHoje.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository) {
            _produtoRepository = produtoRepository;
        }

        // GET /produtos/categoria/{categoriaId}
        [HttpGet("categoria/{categoriaId}")]
        public async Task<IActionResult> GetProdutosPorCategoria(int categoriaId) {
            try {
                var produtos = await _produtoRepository.ObterProdutosPorCategoriaAsync(categoriaId);

                if (produtos == null || !produtos.Any()) {
                    return NotFound(new { Message = "Nenhum produto encontrado para a categoria especificada." });
                }

                return Ok(produtos);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar produtos por categoria.", Detalhes = ex.Message });
            }
        }

        //// GET /produtos/promocoes
        //[HttpGet("promocoes")]
        //public async Task<IActionResult> GetProdutosPromocao() {
        //    try {
        //        var produtos = await _produtoRepository.ObterProdutosPromocaoAsync();

        //        if (produtos == null || !produtos.Any()) {
        //            return NotFound(new { Message = "Nenhum produto em promoção encontrado." });
        //        }

        //        return Ok(produtos);
        //    } catch (Exception ex) {
        //        return StatusCode(500, new { Message = "Erro ao pesquisar produtos em promoção.", Detalhes = ex.Message });
        //    }
        //}

        //// GET /produtos/mais-vendidos
        //[HttpGet("mais-vendidos")]
        //public async Task<IActionResult> GetProdutosMaisVendidos() {
        //    try {
        //        var produtos = await _produtoRepository.ObterProdutosMaisVendidosAsync();

        //        if (produtos == null || !produtos.Any()) {
        //            return NotFound(new { Message = "Nenhum produto mais vendido encontrado." });
        //        }

        //        return Ok(produtos);
        //    } catch (Exception ex) {
        //        return StatusCode(500, new { Message = "Erro ao pesquisar produtos mais vendidos.", Detalhes = ex.Message });
        //    }
        //}

        //// GET /produtos/{id}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetDetalheProduto(int id) {
        //    try {
        //        var produto = await _produtoRepository.ObterDetalheProdutoAsync(id);

        //        if (produto == null) {
        //            return NotFound(new { Message = "Produto não encontrado." });
        //        }

        //        return Ok(produto);
        //    } catch (Exception ex) {
        //        return StatusCode(500, new { Message = "Erro ao pesquisar detalhes do produto.", Detalhes = ex.Message });
        //    }
        //}

        //// GET /produtos
        //[HttpGet]
        //public async Task<IActionResult> GetTodosProdutos() {
        //    try {
        //        var produtos = await _produtoRepository.ObterTodosProdutosAsync();

        //        if (produtos == null || !produtos.Any()) {
        //            return NotFound(new { Message = "Nenhum produto encontrado." });
        //        }

        //        return Ok(produtos);
        //    } catch (Exception ex) {
        //        return StatusCode(500, new { Message = "Erro ao pesquisar todos os produtos.", Detalhes = ex.Message });
        //    }
        //}
    }
}
