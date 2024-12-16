using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories
{
    public interface IProdutoRepository {
        Task<IEnumerable<Produto>> ObterProdutosPorCategoriaAsync(int categoriaId);
        //Task<IEnumerable<Produto>> ObterProdutosPromocaoAsync();
        //Task<IEnumerable<Produto>> ObterProdutosMaisVendidosAsync();
        //Task<Produto> ObterDetalheProdutoAsync(int id);
        //Task<IEnumerable<Produto>> ObterTodosProdutosAsync();
    }
}
