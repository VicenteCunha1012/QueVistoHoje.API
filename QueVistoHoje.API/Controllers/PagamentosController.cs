using Microsoft.AspNetCore.Mvc;
using QueVistoHoje.API.Repositories.Pagamentos;

namespace QueVistoHoje.API.Controllers {
    [ApiController]
    [Route("api/pagamentos")]
    public class PagamentosController : ControllerBase {
        private readonly IPagamentoRepository IRepository;
        public PagamentosController(IPagamentoRepository IRepository) {
            this.IRepository = IRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagamentos() {
            try {
                var pagamentos = await this.IRepository.GetPagamentosAsync();

                if (pagamentos == null || !pagamentos.Any()) {
                    return NotFound(new { Message = "Nenhuma empresa encontrada." });
                }
                return Ok(pagamentos);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar todos os pagamentos.", Detalhes = ex.Message });
            }
        }


    }
}
