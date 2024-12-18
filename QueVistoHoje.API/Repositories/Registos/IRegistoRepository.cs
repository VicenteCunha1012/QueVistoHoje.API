using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Registos {
    public interface IRegistoRepository {
        Task<List<Registo>> GetRegistosAsync();
    }
}
