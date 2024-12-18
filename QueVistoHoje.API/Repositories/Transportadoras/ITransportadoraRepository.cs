using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Transportadoras {
    public interface ITransportadoraRepository {
        Task<List<Transportadora>> GetTransportadorasAsync();
    }
}
