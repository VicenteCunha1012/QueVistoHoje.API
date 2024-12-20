using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueVistoHoje.API.Repositories.Empresas;

namespace QueVistoHoje.API.Controllers {
    [Route("api/empresas")]
    [ApiController]
    [AllowAnonymous]
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

        // GET /api/empresas/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpresaById(int id) {
            try {
                var empresa = await IRepository.GetEmpresaByIdAsync(id);

                if (empresa == null) {
                    return NotFound(new { Message = "Empresa não encontrada." });
                }

                return Ok(empresa);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisasr empresa por ID.", Detalhes = ex.Message });
            }
        }

        // GET /api/empresas/nome/{name}
        [HttpGet("nome/{name}")]
        public async Task<IActionResult> GetEmpresasByName(string name) {
            try {
                var empresas = await IRepository.GetEmpresasByNameAsync(name.Trim());

                if (empresas == null || empresas.Count == 0) {
                    return NotFound(new { Message = "Nenhuma empresa encontrada com esse nome." });
                }

                return Ok(empresas);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisasr empresas pelo nome.", Detalhes = ex.Message });
            }
        }
    }
}
