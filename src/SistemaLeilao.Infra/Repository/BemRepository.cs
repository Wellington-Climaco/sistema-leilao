using SistemaLeilao.Core;
using SistemaLeilao.Core.Entities;
using SistemaLeilao.Core.Interface;
using SistemaLeilao.Infra.ContextDb;

namespace SistemaLeilao.Infra.Repository;

public class BemRepository : IBemRepository
{
    private readonly AppDbContext _dbContext;

    public BemRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Save(Bem entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}