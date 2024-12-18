using Microsoft.EntityFrameworkCore;
using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Transportadoras {
    public class TransportadoraRepository : ITransportadoraRepository {
        private readonly AppDbContext context;
        public TransportadoraRepository(AppDbContext context) {
            this.context = context;
        }
        public async Task<List<Transportadora>> GetTransportadorasAsync() {
            return await context.Transportadoras.ToListAsync();
        }

    }
}
