using Microsoft.AspNetCore.Mvc;
using QueVistoHoje.API.Repositories.Empresas;
using QueVistoHoje.API.Repositories.Transportadoras;

namespace QueVistoHoje.API.Controllers {
    [ApiController]
    [Route("api/transportadoras")]
    public class TransportadorasController : ControllerBase {
        private readonly ITransportadoraRepository IRepository;
        public TransportadorasController(ITransportadoraRepository IRepository) {
            this.IRepository = IRepository;
        }
        // GET /transportadoras
        [HttpGet]
        public async Task<IActionResult> GetTransportadoras() {
            try {
                var empresas = await this.IRepository.GetTransportadorasAsync();

                if (empresas == null || !empresas.Any()) {
                    return NotFound(new { Message = "Nenhuma transportadora encontrada." });
                }
                return Ok(empresas);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar todas as transportadoras.", Detalhes = ex.Message });
            }
        }
    }
}
