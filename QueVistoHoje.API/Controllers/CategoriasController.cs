using Microsoft.AspNetCore.Mvc;
using QueVistoHoje.API.Entities;
using QueVistoHoje.API.Repositories;

namespace QueVistoHoje.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriasController(ICategoriaRepository categoriaRepository) {
            _categoriaRepository = categoriaRepository;
        }

        // GET /categorias: Listar categorias e subcategorias
        [HttpGet]
        public async Task<IActionResult> GetCategorias() {
            try {
                var categorias = await _categoriaRepository.ObterCategoriasAsync();

                if (categorias == null || !categorias.Any()) {
                    return NotFound(new { Message = "Nenhuma categoria encontrada." });
                }

                return Ok(categorias);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao obter categorias.", Detalhes = ex.Message });
            }
        }

        // POST /categorias: Criar nova categoria
        [HttpPost]
        public async Task<IActionResult> CriarCategoria([FromBody] Categoria novaCategoria) {
            if (novaCategoria == null || string.IsNullOrWhiteSpace(novaCategoria.Nome)) {
                return BadRequest(new { Message = "Dados inválidos para criar uma categoria." });
            }

            try {
                var categoriaCriada = await _categoriaRepository.CriarCategoriaAsync(novaCategoria);
                return CreatedAtAction(nameof(GetCategorias), new { id = categoriaCriada.Id }, categoriaCriada);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao criar categoria.", Detalhes = ex.Message });
            }
        }

        // PUT /categorias/{id}: Atualizar uma categoria
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCategoria(int id, [FromBody] Categoria categoriaAtualizada) {
            if (categoriaAtualizada == null || string.IsNullOrWhiteSpace(categoriaAtualizada.Nome)) {
                return BadRequest(new { Message = "Dados inválidos para atualizar a categoria." });
            }

            try {
                var categoriaExistente = await _categoriaRepository.ObterCategoriaPorIdAsync(id);

                if (categoriaExistente == null) {
                    return NotFound(new { Message = "Categoria não encontrada." });
                }

                await _categoriaRepository.AtualizarCategoriaAsync(id, categoriaAtualizada);
                return NoContent();
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao atualizar categoria.", Detalhes = ex.Message });
            }
        }

        // DELETE /categorias/{id}: Remover categoria (se não houver dependências)
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverCategoria(int id) {
            try {
                var categoriaExistente = await _categoriaRepository.ObterCategoriaPorIdAsync(id);

                if (categoriaExistente == null) {
                    return NotFound(new { Message = "Categoria não encontrada." });
                }

                var possuiDependencias = await _categoriaRepository.PossuiDependenciasAsync(id);
                if (possuiDependencias) {
                    return BadRequest(new { Message = "Categoria não pode ser removida pois possui dependências." });
                }

                await _categoriaRepository.RemoverCategoriaAsync(id);
                return NoContent();
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro ao remover categoria.", Detalhes = ex.Message });
            }
        }
    }
}
