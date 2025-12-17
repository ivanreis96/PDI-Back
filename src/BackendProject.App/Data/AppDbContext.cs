using BackendProject.App.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.App.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Receita> Receitas => Set<Receita>();
    public DbSet<Ingrediente> Ingredientes => Set<Ingrediente>();
    public DbSet<ReceitaIngrediente> ReceitaIngredientes => Set<ReceitaIngrediente>();
    public DbSet<PublicacaoReceita> PublicacoesReceitas => Set<PublicacaoReceita>();
    public DbSet<ReceitaDeliciosa> ReceitasDeliciosas => Set<ReceitaDeliciosa>();
    public DbSet<ReceitaAmada> ReceitasAmadas => Set<ReceitaAmada>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ReceitaIngrediente>()
            .HasKey(ri => new { ri.ReceitaId, ri.IngredienteId });

        modelBuilder.Entity<ReceitaIngrediente>()
            .HasOne(ri => ri.Receita)
            .WithMany(r => r.ReceitaIngredientes)
            .HasForeignKey(ri => ri.ReceitaId);

        modelBuilder.Entity<ReceitaIngrediente>()
            .HasOne(ri => ri.Ingrediente)
            .WithMany(i => i.ReceitaIngredientes)
            .HasForeignKey(ri => ri.IngredienteId);

        modelBuilder.Entity<Receita>()
            .HasOne(r => r.Usuario)
            .WithMany(u => u.Receitas)
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PublicacaoReceita>()
            .HasOne(p => p.Receita)
            .WithOne(r => r.Publicacao)
            .HasForeignKey<PublicacaoReceita>(p => p.ReceitaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ReceitaDeliciosa>()
            .HasKey(rd => new { rd.UsuarioId, rd.ReceitaId });

        modelBuilder.Entity<ReceitaDeliciosa>()
            .HasOne(rd => rd.Usuario)
            .WithMany(u => u.ReceitasDeliciosas)
            .HasForeignKey(rd => rd.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ReceitaDeliciosa>()
            .HasOne(rd => rd.Receita)
            .WithMany(r => r.UsuariosQueAprovaram)
            .HasForeignKey(rd => rd.ReceitaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ReceitaAmada>()
            .HasKey(ra => new { ra.UsuarioId, ra.ReceitaId });

        modelBuilder.Entity<ReceitaAmada>()
            .HasOne(ra => ra.Usuario)
            .WithMany(u => u.ReceitasAmadas)
            .HasForeignKey(ra => ra.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ReceitaAmada>()
            .HasOne(ra => ra.Receita)
            .WithMany(r => r.UsuariosQueAmaram)
            .HasForeignKey(ra => ra.ReceitaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Apelido)
            .IsUnique();

        modelBuilder.Entity<Ingrediente>()
            .HasIndex(i => i.Nome);

        modelBuilder.Entity<PublicacaoReceita>()
            .HasIndex(p => p.DataPublicacao);

        modelBuilder.Entity<PublicacaoReceita>()
            .Property(p => p.NotaMedia)
            .HasPrecision(3, 2);
    }
}
