using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueVistoHoje.API.Repositories.Categorias;

namespace QueVistoHoje.API.Controllers {
    [Route("api/categorias")]
    [ApiController]
    [AllowAnonymous]
    public class CategoriasController : ControllerBase {
        private readonly ICategoriaRepository IRepository;
        public CategoriasController(ICategoriaRepository IRepository) {
            this.IRepository = IRepository;
        }

        // GET /categorias
        [HttpGet]
        public async Task<IActionResult> GetCategorias() {
            try {
                var produtos = await IRepository.GetCategoriasAsync();

                if (produtos == null || !produtos.Any()) {
                    return NotFound(new { Message = "Nenhuma categoria encontrada." });
                }

                return Ok(produtos);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar todas as categorias.", Detalhes = ex.Message });
            }
        }

        // GET /api/categorias/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriaById(int id) {
            try {
                var categoria = await IRepository.GetCategoriaByIdAsync(id);

                if (categoria == null) {
                    return NotFound(new { Message = "Categoria não encontrada." });
                }

                return Ok(categoria);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao buscar categoria por ID.", Detalhes = ex.Message });
            }
        }

        // GET /categorias/search?query={searchTerm}
        [HttpGet("search")]
        public async Task<IActionResult> SearchCategorias([FromQuery] string query) {
            var categorias = await IRepository.GetCategoriasByNameAsync(query.Trim());

            if (categorias == null || !categorias.Any()) {
                return NotFound(new { Message = $"Nenhuma categoria encontrada para '{query}'." });
            }

            return Ok(categorias);
        }

        // GET /api/categorias/pai/{categoriaPaiId}
        [HttpGet("pai/{categoriaPaiId}")]
        public async Task<IActionResult> GetCategoriasByPai(int categoriaPaiId) {
            try {
                var categorias = await IRepository.GetCategoriasByPaiAsync(categoriaPaiId);

                if (categorias == null || !categorias.Any()) {
                    return NotFound(new { Message = "Nenhuma categoria filha encontrada para a categoria pai especificada." });
                }

                return Ok(categorias);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar categorias filhas.", Detalhes = ex.Message });
            }
        }

    }
}
