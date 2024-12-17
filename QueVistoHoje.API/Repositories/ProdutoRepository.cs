using QueVistoHoje.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace QueVistoHoje.API.Repositories {
    public class ProdutoRepository : IProdutoRepository {
        private readonly AppDbContext _context;
        public ProdutoRepository(AppDbContext context) {
            _context = context;
        }
        public async Task<List<Produto>> GetTodosProdutosAsync() {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .ToListAsync();
        }
        public async Task<Produto?> GeteProdutoByIdAsync(int id) {
            return await _context.Produtos.FindAsync(id);
        }
        public async Task<List<Produto>> GetProdutosByCategoryAsync(int categoryId) {
            return await _context.Produtos
                .Where(p => p.Categoria.Any(c => c.Id == categoryId))
                .Include(p => p.Categoria)
                .ToListAsync();
        }

    }
}
