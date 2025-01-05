using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using QueVistoHoje.API.Data.Entities;
using QueVistoHoje.API.Repositories.Encomendas;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;


namespace QueVistoHoje.API.Controllers {

    [Route("api/encomendas")]
    [ApiController]
    [AllowAnonymous]
    public class EncomendasController : ControllerBase {
        private readonly IEncomendaRepository IRepository;
        public EncomendasController(IEncomendaRepository IRepository) {
            this.IRepository = IRepository;
        }

        // GET /encomendas
        [HttpGet]
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

        //POST /api/encomenda/make
        [HttpPost("make")]
        public async Task<IActionResult> CriarEncomenda([FromBody] EncomendaRequest request) {

            if (request == null) {
                return BadRequest(new { Message = "Invalid JSON payload" });
            }

            try {

                Console.WriteLine($"Received request: {System.Text.Json.JsonSerializer.Serialize(request)}");

                if (string.IsNullOrEmpty(request.TokenRequest)) {
                    return Unauthorized(new { Message = "Token é obrigatório." });
                }

                if (request.Encomenda == null) {
                    return BadRequest(new { Message = "A encomenda não pode ser nula." });
                }

                if (string.IsNullOrEmpty(request.Encomenda.EnderecoEntrega)) {
                    return BadRequest(new { Message = "O endereço de entrega é obrigatório." });
                }

                var createdEncomenda = await IRepository.CriarEncomendaAsync(request.Encomenda);

                if (createdEncomenda == null) {
                    return StatusCode(500, new { Message = "Erro ao criar a encomenda." });
                }

                return CreatedAtAction(nameof(GetEncomendas), new { id = createdEncomenda.Id }, createdEncomenda);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao criar a encomenda.", Detalhes = ex.Message });
            }
        }

        public class EncomendaRequest {

            [JsonPropertyName("encomenda")]
            public Encomenda Encomenda { get; set; }

            [JsonPropertyName("tokenRequest")]
            public string TokenRequest { get; set; }

       
        }

    }


}
