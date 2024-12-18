using Microsoft.AspNetCore.Mvc;
using QueVistoHoje.API.Repositories.Produtos;

namespace QueVistoHoje.API.Controllers {
    [ApiController]
    [Route("api/produtos")]
    public class ProdutosController : ControllerBase {
        private readonly IProdutoRepository IRepository;
        public ProdutosController(IProdutoRepository IRepository) {
            this.IRepository = IRepository;
        }

        // GET /produtos
        [HttpGet]
        public async Task<IActionResult> GetProdutos() {
            try {
                var produtos = await IRepository.GetProdutosAsync();

                if (produtos == null || !produtos.Any()) {
                    return NotFound(new { Message = "Nenhum produto encontrado." });
                }

                return Ok(produtos);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar todos os produtos.", Detalhes = ex.Message });
            }
        }

        //// GET /produtos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GeteProdutoById(int id) {
            try {
                var produto = await IRepository.GetProdutoPorIdAsync(id);

                if (produto == null) {
                    return NotFound(new { Message = "Produto não encontrado." });
                }

                return Ok(produto);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar detalhes do produto.", Detalhes = ex.Message });
            }
        }

        // GET /produtos/categoria/{categoriaId}
        [HttpGet("categoria/{categoriaId}")]
        public async Task<IActionResult> GetProdutosPorCategoria(int categoriaId) {
            try {
                var produtos = await IRepository.GetProdutosPorCategoriaAsync(categoriaId);

                if (produtos == null || !produtos.Any()) {
                    return NotFound(new { Message = "Nenhum produto encontrado para a categoria especificada." });
                }

                return Ok(produtos);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar produtos por categoria.", Detalhes = ex.Message });
            }
        }

        // GET /produtos/promocoes
        [HttpGet("promocoes")]
        public async Task<IActionResult> GetProdutosEmPromocao() {
            try {
                var produtos = await IRepository.GetProdutosEmPromocao();

                if (produtos == null || !produtos.Any()) {
                    return NotFound(new { message = "Nenhum produto em promoção encontrado." });
                }

                return Ok(produtos);
            } catch (Exception ex) {
                return StatusCode(500, new { message = "Erro ao pesquisar produtos em promoção.", Detalhes = ex.Message });
            }
        }

        // GET /produtos/top-baratos/{n}
        [HttpGet("top-baratos/{n}")]
        public async Task<IActionResult> GetTopNProdutosMaisBaratos(int n) {
            try {
                var produtos = await IRepository.GetTopNProdutosMaisBaratosAsync(n);

                if (produtos == null || !produtos.Any()) {
                    return NotFound(new { Message = "Nenhum produto encontrado." });
                }

                return Ok(produtos);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar os produtos mais baratos.", Detalhes = ex.Message });
            }
        }

        // GET /produtos/top-caros/{n}
        [HttpGet("top-caros/{n}")]
        public async Task<IActionResult> GetTopNProdutosMaisCaros(int n) {
            try {
                var produtos = await IRepository.GetTopNProdutosMaisCarosAsync(n);

                if (produtos == null || !produtos.Any()) {
                    return NotFound(new { Message = "Nenhum produto encontrado." });
                }

                return Ok(produtos);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar os produtos mais caros.", Detalhes = ex.Message });
            }
        }

        // GET /produtos/preco-minimo?min={precoMinimo}
        [HttpGet("preco-minimo")]
        public async Task<IActionResult> GetProdutosPrecoMinimo([FromQuery] decimal min) {
            try {
                var produtos = await IRepository.GetProdutosPrecoMinimoAsync(min);

                if (produtos == null || !produtos.Any()) {
                    return NotFound(new { Message = "Nenhum produto encontrado acima ou igual ao preço especificado." });
                }

                return Ok(produtos);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar produtos pelo preço mínimo.", Detalhes = ex.Message });
            }
        }

        // GET /produtos/preco-maximo?max={precoMaximo}
        [HttpGet("preco-maximo")]
        public async Task<IActionResult> GetProdutosPrecoMaximo([FromQuery] decimal max) {
            try {
                var produtos = await IRepository.GetProdutosPrecoMaximoAsync(max);

                if (produtos == null || !produtos.Any()) {
                    return NotFound(new { Message = "Nenhum produto encontrado abaixo ou igual ao preço especificado." });
                }

                return Ok(produtos);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar produtos pelo preço máximo.", Detalhes = ex.Message });
            }
        }

        [HttpGet("preco-range")]
        public async Task<IActionResult> GetProdutosRangePreco([FromQuery] decimal min, [FromQuery] decimal max) {
            try {
                var produtos = await IRepository.GetProdutosPorRangeDePrecoAsync(min, max);

                if (produtos == null || !produtos.Any()) {
                    return NotFound(new { Message = "Nenhum produto encontrado na faixa de preço especificada." });
                }

                return Ok(produtos);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar produtos na faixa de preço.", Detalhes = ex.Message });
            }
        }

        // GET /produtos/search?query={searchTerm}
        [HttpGet("search")]
        public async Task<IActionResult> SearchProdutos([FromQuery] string query) {
            try {
                var produtos = await IRepository.GetProdutosByStringAsync(query.Trim());

                if (produtos == null || !produtos.Any()) {
                    return NotFound(new { Message = $"Nenhum produto encontrado que corresponda ao termo '{query}'." });
                }

                return Ok(produtos);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar produtos pelo termo fornecido.", Detalhes = ex.Message });
            }
        }

        [HttpGet("inStock")]
        public async Task<IActionResult> GetProdutosInstock() {
            try {
                var produtos = await IRepository.GetProdutosEmStockAsync();

                if (produtos == null || !produtos.Any()) {
                    return NotFound(new { Message = "Esta empresa não possui stock de nada..." });
                }

                return Ok(produtos);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar produtos em stock.", Detalhes = ex.Message });
            }
        }

    }
}
