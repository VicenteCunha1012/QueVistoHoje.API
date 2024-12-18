using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Encomendas {
    public interface IEncomendaRepository {
        Task<List<Encomenda>> GetEncomendasAsync();

    }
}
