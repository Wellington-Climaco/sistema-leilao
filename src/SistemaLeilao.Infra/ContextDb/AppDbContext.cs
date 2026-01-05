using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SistemaLeilao.Core;
using SistemaLeilao.Core.Entities;
using SistemaLeilao.Core.Enum;
using SistemaLeilao.Core.ValueObject;

namespace SistemaLeilao.Infra.ContextDb;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Bem> Bens { get; set; }
    public DbSet<Lance> Lances { get; set; }
    public DbSet<Leilao> Leiloes { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Imagem> Imagens { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Bem>().HasKey(x=>x.Id);
        modelBuilder.Entity<Bem>().Property(x => x.Id).ValueGeneratedNever();
        
        modelBuilder.Entity<Lance>().HasKey(x=>x.Id);
        modelBuilder.Entity<Lance>().Property(x => x.Id).ValueGeneratedNever();
        
        modelBuilder.Entity<Leilao>().HasKey(x=>x.Id);
        modelBuilder.Entity<Leilao>().Property(x => x.Id).ValueGeneratedNever();
        modelBuilder.Entity<Leilao>().Property(x => x.Encerramento).HasColumnName("Encerramento");
        modelBuilder.Entity<Leilao>().Property(x => x.Status).HasConversion(v => v.ToString(),v => (StatusLeilao)Enum.Parse(typeof(StatusLeilao), v));
        modelBuilder.Entity<Leilao>().Property(x => x.Encerramento).HasColumnType("DATETIME");
        modelBuilder.Entity<Leilao>().Property(x => x.CreatedAt).HasColumnType("DATETIME");
        
        modelBuilder.Entity<Usuario>().HasKey(x=>x.Id);
        modelBuilder.Entity<Usuario>().Property(x => x.Id).ValueGeneratedNever();
        modelBuilder.Entity<Usuario>().OwnsOne(x => x.Email).Property(x => x.EmailAdress).HasColumnName("Email");
        
        modelBuilder.Entity<Imagem>().HasKey(x=>x.Id);
        modelBuilder.Entity<Imagem>().Property(x => x.Id).ValueGeneratedNever();
        modelBuilder.Entity<Imagem>().Property(x => x.CreatedAt).HasColumnType("DATETIME");

    }
}