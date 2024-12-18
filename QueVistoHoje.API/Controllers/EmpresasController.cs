using Microsoft.AspNetCore.Mvc;
using QueVistoHoje.API.Repositories.Empresas;

namespace QueVistoHoje.API.Controllers {
    [ApiController]
    [Route("api/empresas")]
    public class EmpresasController : ControllerBase {
        private readonly IEmpresaRepository IRepository;
        public EmpresasController(IEmpresaRepository IRepository) {
            this.IRepository = IRepository;
        }
        // GET /empresas
        [HttpGet]
        public async Task<IActionResult> GetEmpresas() {
            try {
                var empresas = await this.IRepository.GetEmpresasAsync();

                if (empresas == null || !empresas.Any()) {
                    return NotFound(new { Message = "Nenhuma empresa encontrada." });
                }
                return Ok(empresas);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar todas as empresas.", Detalhes = ex.Message });
            }
        }
    }
}
