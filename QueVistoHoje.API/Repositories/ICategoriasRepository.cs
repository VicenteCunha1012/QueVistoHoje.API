using System.Collections.Generic;
using System.Threading.Tasks;
using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories
{
    public interface ICategoriaRepository {
        Task<IEnumerable<Categoria>> ObterCategoriasAsync();
        Task<Categoria> ObterCategoriaPorIdAsync(int id);
        Task<Categoria> CriarCategoriaAsync(Categoria novaCategoria);
        Task<bool> AtualizarCategoriaAsync(int id, Categoria categoriaAtualizada);
        Task<bool> RemoverCategoriaAsync(int id);
        Task<bool> PossuiDependenciasAsync(int id);
    }
}