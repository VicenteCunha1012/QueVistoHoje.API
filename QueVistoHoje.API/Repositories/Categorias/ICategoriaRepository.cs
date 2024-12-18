using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Categorias {
    public interface ICategoriaRepository {
        Task<List<Categoria>> GetCategoriasAsync();
        Task<Categoria?> GetCategoriaByIdAsync(int id);
        Task<List<Categoria>> GetCategoriasByNameAsync(string searchTerm);
        Task<List<Categoria>> GetCategoriasByPaiAsync(int categoriaPaiId);
    }
}
