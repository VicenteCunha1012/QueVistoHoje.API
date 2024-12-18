using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Categorias
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> GetCategoriasAsync();
    }
}
