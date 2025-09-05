using Microsoft.EntityFrameworkCore;
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

    public async Task<Leilao?> FindById(Guid id)
    {
        var result = await _dbContext.Leiloes.Include(x=>x.Bem).FirstOrDefaultAsync(x=>x.Id == id);
        return result;
    }

    public async Task InitializeLeilao(Leilao leilao)
    {
        _dbContext.Leiloes.Update(leilao);
        await _dbContext.SaveChangesAsync();
    }
}