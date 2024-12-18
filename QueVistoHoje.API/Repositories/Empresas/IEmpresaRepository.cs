using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Empresas {
    public interface IEmpresaRepository {
        Task<List<Empresa>> GetEmpresasAsync();

    }
}
