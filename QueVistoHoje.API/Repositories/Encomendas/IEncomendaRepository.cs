using QueVistoHoje.API.Data.Entities;

namespace QueVistoHoje.API.Repositories.Encomendas {
    public interface IEncomendaRepository {
        Task<List<Encomenda>> GetEncomendasAsync();
        Task<Encomenda> CriarEncomendaAsync(Encomenda encomenda);
    }
}
