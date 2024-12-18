using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using QueVistoHoje.API.Data;
using QueVistoHoje.API.Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options) {
    public DbSet<Categoria> Categorias { get; set; }    // done
    public DbSet<Empresa> Empresas { get; set; }        // done
    public DbSet<Encomenda> Encomendas { get; set; }    // done
    public DbSet<Pagamento> Pagamentos { get; set; }    // started
    public DbSet<Produto> Produtos { get; set; }        // done
    public DbSet<Registo> Registos { get; set; }        // started
    public DbSet<Transportadora> Transportadoras { get; set; } // started
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Produto>()
            .HasMany(p => p.Categoria)
            .WithMany(c => c.Produtos)
            .UsingEntity(j => j.ToTable("categoriaProduto"));
    }

}


