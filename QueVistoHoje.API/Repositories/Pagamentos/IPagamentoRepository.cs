using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Pagamentos {
    public interface IPagamentoRepository {
        Task<List<Pagamento>> GetPagamentosAsync();

    }
}
