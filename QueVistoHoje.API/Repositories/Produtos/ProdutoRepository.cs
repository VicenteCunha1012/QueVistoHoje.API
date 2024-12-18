using QueVistoHoje.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace QueVistoHoje.API.Repositories.Produtos {
    public class ProdutoRepository : IProdutoRepository {
        private readonly AppDbContext context;
        public ProdutoRepository(AppDbContext context) {
            this.context = context;
        }

        public async Task<List<Produto>> GetProdutosAsync() {
            return await context.Produtos
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        public async Task<Produto?> GetProdutoPorIdAsync(int id) {
            return await context.Produtos.FindAsync(id);
        }

        public async Task<List<Produto>> GetProdutosPorCategoriaAsync(int categoryId) {
            return await context.Produtos
                .Where(p => p.Categoria.Any(c => c.Id == categoryId))
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        public async Task<List<Produto>> GetProdutosEmPromocao() {
            return await context.Produtos
                .Where(p => p.Promocao == true)
                .ToListAsync();
        }

        public async Task<List<Produto>> GetTopNProdutosMaisBaratosAsync(int n) {
            return await context.Produtos
                .OrderBy(p => p.Preco)
                .Take(n)
                .ToListAsync();
        }

        public async Task<List<Produto>> GetTopNProdutosMaisCarosAsync(int n) {
            return await context.Produtos
                .OrderByDescending(p => p.Preco)
                .Take(n)
                .ToListAsync();
        }

        public async Task<List<Produto>> GetProdutosPrecoMinimoAsync(decimal precoMinimo) {
            return await context.Produtos
                .Where(p => p.Preco >= precoMinimo)
                .OrderBy(p => p.Preco)
                .ToListAsync();
        }

        public async Task<List<Produto>> GetProdutosPrecoMaximoAsync(decimal precoMaximo) {
            return await context.Produtos
                .Where(p => p.Preco <= precoMaximo)
                .OrderBy(p => p.Preco)
                .ToListAsync();
        }

        public async Task<List<Produto>> GetProdutosPorRangeDePrecoAsync(decimal precoMinimo, decimal precoMaximo) {
            return await context.Produtos
                .Where(p => p.Preco >= precoMinimo && p.Preco <= precoMaximo)
                .OrderBy(p => p.Preco)
                .ToListAsync();
        }

        public async Task<List<Produto>> GetProdutosByStringAsync(string searchTerm) {
            return await context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Empresa)
                .Where(p =>
                    EF.Functions.Like(p.Nome, $"%{searchTerm}%") ||
                    EF.Functions.Like(p.Descricao, $"%{searchTerm}%") ||
                    p.Categoria.Any(c => EF.Functions.Like(c.Nome, $"%{searchTerm}%")) ||
                    EF.Functions.Like(p.Empresa.Nome, $"%{searchTerm}%")
                )
                .ToListAsync();
        }

        public async Task<List<Produto>> GetProdutosEmStockAsync() {
            return await context.Produtos
                      .Include(p => p.Categoria)
                      .Where(p => p.Stock > 0)
                      .ToListAsync();
        }
    }
}