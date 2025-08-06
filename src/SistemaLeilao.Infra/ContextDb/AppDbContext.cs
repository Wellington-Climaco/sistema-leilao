using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SistemaLeilao.Core;
using SistemaLeilao.Core.Entities;
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
        
        modelBuilder.Entity<Usuario>().HasKey(x=>x.Id);
        modelBuilder.Entity<Usuario>().Property(x => x.Id).ValueGeneratedNever();
        modelBuilder.Entity<Usuario>().OwnsOne(x => x.Email).Property(x => x.EmailAdress).HasColumnName("Email");

    }
}