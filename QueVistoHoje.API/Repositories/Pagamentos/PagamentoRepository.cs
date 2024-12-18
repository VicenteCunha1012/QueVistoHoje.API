using Microsoft.EntityFrameworkCore;
using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Pagamentos {
    public class PagamentoRepository : IPagamentoRepository {
        private readonly AppDbContext context;
        public PagamentoRepository(AppDbContext context) {
            this.context = context;
        }
        public async Task<List<Pagamento>> GetPagamentosAsync() {
            return await context.Pagamentos.ToListAsync();
        }
    }
}
