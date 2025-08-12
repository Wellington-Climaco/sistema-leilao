using SistemaLeilao.Core;
using SistemaLeilao.Core.Interface;
using SistemaLeilao.Infra.ContextDb;

namespace SistemaLeilao.Infra.Repository;

public class LeilaoRepository : ILeilaoRepository
{
    private readonly AppDbContext _dbContext;

    public LeilaoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task RegisterLeilao(Leilao leilao)
    {
        await _dbContext.Leiloes.AddAsync(leilao);
        await _dbContext.SaveChangesAsync();
    }
}