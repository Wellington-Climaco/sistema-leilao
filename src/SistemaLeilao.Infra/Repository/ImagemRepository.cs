using Microsoft.EntityFrameworkCore;
using SistemaLeilao.Core.Entities;
using SistemaLeilao.Core.Interface;
using SistemaLeilao.Infra.ContextDb;

namespace SistemaLeilao.Infra.Repository;

internal class ImagemRepository : IImagemRepository
{
    private readonly AppDbContext _dbContext;

    public ImagemRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Save(Imagem imagem)
    {
        throw new NotImplementedException();
    }

    public async Task SaveCollection(List<Imagem> imagem)
    {
        await _dbContext.AddRangeAsync(imagem);
        await _dbContext.SaveChangesAsync();
    }
}
