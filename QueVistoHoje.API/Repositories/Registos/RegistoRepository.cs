using Microsoft.EntityFrameworkCore;
using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Registos {
    public class RegistoRepository : IRegistoRepository {
        private readonly AppDbContext context;
        public RegistoRepository(AppDbContext context) {
            this.context = context;
        }
        public async Task<List<Registo>> GetRegistosAsync() {
            return await context.Registos.ToListAsync();

        }

    }
}
