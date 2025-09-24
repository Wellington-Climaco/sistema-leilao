namespace SistemaLeilao.Core.Interface;

public interface ILeilaoRepository
{
    Task RegisterLeilao(Leilao leilao);
    Task<Leilao?> FindById(Guid id);
    Task InitializeLeilao(Leilao leilao);
    Task<(IEnumerable<Leilao> leiloes,int total)> FindByPagination(int skip, int take);
}