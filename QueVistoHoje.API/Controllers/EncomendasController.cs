using Microsoft.AspNetCore.Mvc;
using QueVistoHoje.API.Repositories.Encomendas;

namespace QueVistoHoje.API.Controllers {
    [ApiController]
    [Route("api/encomendas")]
    public class EncomendasController : ControllerBase {
        private readonly IEncomendaRepository IRepository;
        public EncomendasController(IEncomendaRepository IRepository) {
            this.IRepository = IRepository;
        }
        // GET /encomendas
        [HttpGet]
        public async Task<IActionResult> GetEncomendas() {
            try {
                var encomendas = await this.IRepository.GetEncomendasAsync();

                if (encomendas == null || !encomendas.Any()) {
                    return NotFound(new { Message = "Nenhuma empresa encontrada." });
                }
                return Ok(encomendas);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar todas as empresas.", Detalhes = ex.Message });
            }
        }
    }
}
