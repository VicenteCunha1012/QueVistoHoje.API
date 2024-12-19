using QueVistoHoje.API.Data.Entities;

namespace QueVistoHoje.API.Repositories.Encomendas {
    public interface IEncomendaRepository {
        Task<List<Encomenda>> GetEncomendasAsync();
        Task<List<Encomenda>> GetClientEncomendasAsync(string clientId);
        Task<List<Encomenda>> GetClientEncomendasSpecificStateAsync(string clientId, EncomendaState state);
        Task<Encomenda> CriarEncomendaAsync(Encomenda encomenda);
    }
}
