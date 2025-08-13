namespace SistemaLeilao.Core.Interface;

public interface ILeilaoRepository
{
    Task RegisterLeilao(Leilao leilao);
    Task<Leilao?> FindById(Guid id);
}