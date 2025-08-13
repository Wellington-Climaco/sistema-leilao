using FluentResults;
using SistemaLeilao.Application.Response.Leilao;

namespace SistemaLeilao.Application.Interface;

public interface ISearchLeilaoUseCase
{
    Task<Result<LeilaoResponse>> Executar(Guid id);
}