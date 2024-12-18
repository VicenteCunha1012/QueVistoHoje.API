using Microsoft.EntityFrameworkCore;
using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Categorias
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext context;
        public CategoriaRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            return await context.Categorias.ToListAsync();
        }
    }
}
