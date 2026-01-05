using SistemaLeilao.Core.Entities;

namespace SistemaLeilao.Core.Interface;

public interface IImagemRepository
{
    Task Save(Imagem imagem);
    Task SaveCollection(List<Imagem> imagem);
    Task<List<Imagem>> GetImagesByBemId(Guid bemId);
}
