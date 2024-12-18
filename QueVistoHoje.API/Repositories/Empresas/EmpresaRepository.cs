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
    }
}
