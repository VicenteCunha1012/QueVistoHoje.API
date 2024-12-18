using Microsoft.AspNetCore.Mvc;
using QueVistoHoje.API.Repositories.Empresas;
using QueVistoHoje.API.Repositories.Registos;

namespace QueVistoHoje.API.Controllers {
    [ApiController]
    [Route("api/registos")]
    public class RegistosController : ControllerBase {
        private readonly IRegistoRepository IRepository;
        public RegistosController(IRegistoRepository IRepository) {
            this.IRepository = IRepository;
        }

        // GET /registos
        [HttpGet]
        public async Task<IActionResult> GetRegistos() {
            try {
                var registos = await this.IRepository.GetRegistosAsync();

                if (registos == null || !registos.Any()) {
                    return NotFound(new { Message = "Nenhum registo encontrado." });
                }
                return Ok(registos);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar todos os registos.", Detalhes = ex.Message });
            }
        }

    }
}
