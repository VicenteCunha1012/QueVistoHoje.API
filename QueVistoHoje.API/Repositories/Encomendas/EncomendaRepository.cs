using Microsoft.EntityFrameworkCore;
using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Encomendas {
    public class EncomendaRepository : IEncomendaRepository {
        private readonly AppDbContext context;
        public EncomendaRepository(AppDbContext context) {
            this.context = context;
        }
        public async Task<List<Encomenda>> GetEncomendasAsync() {
            return await context.Encomendas.ToListAsync();
        }

    }
}
