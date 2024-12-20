using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using QueVistoHoje.API.Data.Entities;
using QueVistoHoje.API.Repositories.Encomendas;
using Swashbuckle.AspNetCore.Annotations; 

namespace QueVistoHoje.API.Controllers {

    [Route("api/encomendas")]
    [ApiController]
    public class EncomendasController : ControllerBase {
        private readonly IEncomendaRepository IRepository;
        public EncomendasController(IEncomendaRepository IRepository) {
            this.IRepository = IRepository;
        }

        // GET /encomendas
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetEncomendas() {
            try {
                var encomendas = await IRepository.GetEncomendasAsync();

                if (encomendas == null || !encomendas.Any()) {
                    return NotFound(new { Message = "Nenhuma encomenda encontrada." });
                }
                return Ok(encomendas);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar todas as encomendas.", Detalhes = ex.Message });
            }
        }

        // GET /api/encomendas/{clientId}
        [HttpGet("cliente/{clientId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetClientEncomendas(string clientId) {
            try {
                var encomendas = await IRepository.GetClientEncomendasAsync(clientId);

                if (encomendas == null || !encomendas.Any()) {
                    return NotFound(new { Message = "Nenhuma encomenda encontrada para o cliente especificado." });
                }

                return Ok(encomendas);
            } catch (InvalidOperationException ex) {
                return StatusCode(404, new { Message = $"Não foi encontrado nenhum cliente com o id {clientId}.", Detalhes = ex.Message });

            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar encomendas para o cliente.", Detalhes = ex.Message });
            }
        }

        // GET /api/encomendas/{clientId}/state
        [HttpGet("cliente/{clientId}/state")]
        [AllowAnonymous]
        public async Task<IActionResult> GetClientEncomendasSpecificState(string clientId, [FromQuery] EncomendaState state) {
            try {
                var encomendas = await IRepository.GetClientEncomendasSpecificStateAsync(clientId, state);

                if (encomendas == null || !encomendas.Any()) {
                    return NotFound(new { Message = "Nenhuma encomenda entregue encontrada para o cliente especificado." });
                }

                return Ok(encomendas);
            } catch (InvalidOperationException ex) {
                return StatusCode(404, new { Message = $"Não foi encontrado nenhum cliente com o id {clientId}.", Detalhes = ex.Message });

            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao pesquisar encomendas entregues para o cliente.", Detalhes = ex.Message });
            }
        }

        // POST /api/encomendas/
        [HttpPost("api/encomenda")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> CriarEncomenda([FromBody] Encomenda encomenda) {
            try {
                if (encomenda == null) {
                    return BadRequest(new { Message = "A encomenda não pode ser nula." });
                }

                if (string.IsNullOrEmpty(encomenda.EnderecoEntrega)) {
                    return BadRequest(new { Message = "O endereço de entrega é obrigatório." });
                }

                var createdEncomenda = await IRepository.CriarEncomendaAsync(encomenda);

                if (createdEncomenda == null) {
                    return StatusCode(500, new { Message = "Erro ao criar a encomenda." });
                }

                // Return the created encomenda with a 201 Created status
                return CreatedAtAction(nameof(GetEncomendas), new { id = createdEncomenda.Id }, createdEncomenda);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao criar a encomenda.", Detalhes = ex.Message });
            }
        }

    }


}
