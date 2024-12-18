using Microsoft.EntityFrameworkCore;
using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Categorias {
    public class CategoriaRepository : ICategoriaRepository {
        private readonly AppDbContext context;
        public CategoriaRepository(AppDbContext context) {
            this.context = context;
        }

        public async Task<Categoria?> GetCategoriaByIdAsync(int id) {
            return await context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Categoria>> GetCategoriasAsync() {
            return await context.Categorias.ToListAsync();
        }
        public async Task<List<Categoria>> GetCategoriasByNameAsync(string searchTerm) {
            return await context.Categorias
                .Where(c => EF.Functions.Like(c.Nome, $"%{searchTerm}%"))
                .ToListAsync();
        }

        public async Task<List<Categoria>> GetCategoriasByPaiAsync(int categoriaPaiId) {
            return await context.Categorias
                .Where(c => c.CategoriaPaiId == categoriaPaiId)
                .ToListAsync();
        }
    }
}
