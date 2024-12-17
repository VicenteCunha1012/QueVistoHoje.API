using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using QueVistoHoje.API.Data;
using QueVistoHoje.API.Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options) {
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias {  get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder); // Preserves Identity configurations

        // Your custom many-to-many configuration
        modelBuilder.Entity<Produto>()
            .HasMany(p => p.Categoria)
            .WithMany(c => c.Produtos)
            .UsingEntity(j => j.ToTable("categoriaProduto")); // Custom table name
    }

}


