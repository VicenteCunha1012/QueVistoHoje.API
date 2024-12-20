using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using QueVistoHoje.API.Data;
using QueVistoHoje.API.Data.Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options) {
    public DbSet<Categoria> Categorias { get; set; }    // done
    public DbSet<Empresa> Empresas { get; set; }        // done
    public DbSet<Encomenda> Encomendas { get; set; }    // falta: Fazer Encomenda
    public DbSet<Produto> Produtos { get; set; }        // done
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        // Set precision and scale for 'Preco' property
        modelBuilder.Entity<Produto>()
            .Property(p => p.Preco)
            .HasColumnType("decimal(18,2)");  // Precision 18, Scale 2

        modelBuilder.Entity<Produto>()
            .HasMany(p => p.Categoria)
            .WithMany(c => c.Produtos)
            .UsingEntity(j => j.ToTable("categoriaProduto"));
    }

}


