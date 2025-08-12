namespace SistemaLeilao.Core.Interface;

public interface ILeilaoRepository
{
    Task RegisterLeilao(Leilao leilao);
}