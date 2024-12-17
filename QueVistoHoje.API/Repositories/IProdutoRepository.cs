using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories {
    public interface IProdutoRepository {
        Task<List<Produto>> GetTodosProdutosAsync();
        Task<Produto?> GeteProdutoByIdAsync(int id);
        Task<List<Produto>> GetProdutosByCategoryAsync(int categoryId);
    }
}
