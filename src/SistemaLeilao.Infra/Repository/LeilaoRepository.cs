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

    public async Task<(IEnumerable<Leilao> leiloes, int total)> FindByPagination(int skip, int take)
    {
        var leiloes = await _dbContext.Leiloes.AsNoTracking().Where(x => x.Encerramento > DateTime.Now)
            .Include(x=>x.Bem)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        int count = await _dbContext.Leiloes.AsNoTracking().Where(x => x.Encerramento > DateTime.Now).CountAsync();
        return (leiloes,count);
    }
}