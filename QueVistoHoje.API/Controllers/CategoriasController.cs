using Microsoft.AspNetCore.Mvc;
using QueVistoHoje.API.Repositories.Categorias;

namespace QueVistoHoje.API.Controllers {
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController : ControllerBase {
        private readonly ICategoriaRepository IRepository;
        public CategoriasController(ICategoriaRepository IRepository) {
            this.IRepository = IRepository;
        }

        // GET /categorias
        [HttpGet]
        public async Task<IActionResult> GetCategoriasTodas() {
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

    }
}
