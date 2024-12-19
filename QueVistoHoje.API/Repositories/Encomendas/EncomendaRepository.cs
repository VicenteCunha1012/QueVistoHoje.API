using Microsoft.EntityFrameworkCore;
using QueVistoHoje.API.Data.Entities;

namespace QueVistoHoje.API.Repositories.Encomendas {
    public class EncomendaRepository : IEncomendaRepository {
        private readonly AppDbContext context;
        public EncomendaRepository(AppDbContext context) {
            this.context = context;
        }

        public async Task<List<Encomenda>> GetEncomendasAsync() {
            return await context.Encomendas.ToListAsync();
        }

        public async Task<List<Encomenda>> GetClientEncomendasAsync(string clientId) {

            if (!await context.Users.AnyAsync(c => c.Id == clientId)) {
                throw new InvalidOperationException();
            }

            return await context.Encomendas
                .Where(p => p.Cliente.Id == clientId)
                .ToListAsync();
        }

        public async Task<List<Encomenda>> GetClientEncomendasSpecificStateAsync(string clientId, EncomendaState state) {
            if (!await context.Users.AnyAsync(c => c.Id == clientId)) {
                throw new InvalidOperationException();
            }
            
            return await context.Encomendas
                .Where(p => p.Cliente.Id == clientId && p.Estado == state)
                .ToListAsync();
        }

        public async Task<Encomenda> CriarEncomendaAsync(Encomenda encomenda) {
            context.Encomendas.Add(encomenda);

            await context.SaveChangesAsync();

            return encomenda;
        }
    }
}
