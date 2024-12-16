using QueVistoHoje.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace QueVistoHoje.API.Repositories {
    public class ProdutoRepository : IProdutoRepository {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorCategoriaAsync(int categoriaId) {
            return await _context.Produtos
                .Where(p => p.Categoria.Any(c => c.Id == categoriaId))
            .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPromocaoAsync() {
            return null;
        }
    }
}
