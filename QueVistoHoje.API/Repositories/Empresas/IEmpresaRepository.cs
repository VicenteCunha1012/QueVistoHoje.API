using QueVistoHoje.API.Data.Entities;

namespace QueVistoHoje.API.Repositories.Empresas {
    public interface IEmpresaRepository {
        Task<List<Empresa>> GetEmpresasAsync();
        Task<Empresa?> GetEmpresaByIdAsync(int id);
        Task<List<Empresa>> GetEmpresasByNameAsync(string name);
        }
    }
