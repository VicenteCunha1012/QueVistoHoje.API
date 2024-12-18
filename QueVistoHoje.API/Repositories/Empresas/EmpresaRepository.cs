using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueVistoHoje.API.Entities;

namespace QueVistoHoje.API.Repositories.Empresas {
    public class EmpresaRepository : IEmpresaRepository {
        private readonly AppDbContext context;
        public EmpresaRepository(AppDbContext context) {
            this.context = context;
        }
        public async Task<List<Empresa>> GetEmpresasAsync() {
            return await context.Empresas.ToListAsync();
        }

        public async Task<Empresa?> GetEmpresaByIdAsync(int id) {
            return await context.Empresas
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Empresa>> GetEmpresasByNameAsync(string name) {
            return await context.Empresas
                .Where(e => EF.Functions.Like(e.Nome, $"%{name}%"))
                .ToListAsync();
        }
    }
}
