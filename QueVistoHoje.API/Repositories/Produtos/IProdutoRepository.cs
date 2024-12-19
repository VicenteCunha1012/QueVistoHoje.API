using QueVistoHoje.API.Data.Entities;

namespace QueVistoHoje.API.Repositories.Produtos {
    public interface IProdutoRepository {
        Task<List<Produto>> GetProdutosAsync();
        Task<Produto?> GetProdutoPorIdAsync(int id);
        Task<List<Produto>> GetProdutosPorCategoriaAsync(int categoryId);

        //Task<List<Produto>> GetProdutosEmPromocao();
        Task<List<Produto>> GetTopNProdutosMaisBaratosAsync(int n);
        Task<List<Produto>> GetTopNProdutosMaisCarosAsync(int n);
        Task<List<Produto>> GetProdutosPrecoMinimoAsync(decimal precoMinimo);
        Task<List<Produto>> GetProdutosPrecoMaximoAsync(decimal precoMaximo);
        Task<List<Produto>> GetProdutosPorRangeDePrecoAsync(decimal precoMinimo, decimal precoMaximo);
        Task<List<Produto>> GetProdutosByStringAsync(string searchTerm);
        Task<List<Produto>> GetProdutosEmStockAsync();

    }
}
