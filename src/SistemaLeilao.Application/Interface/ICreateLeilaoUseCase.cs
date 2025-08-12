using FluentResults;
using SistemaLeilao.Application.Request.Leilao;
using SistemaLeilao.Application.Response.Leilao;

namespace SistemaLeilao.Application.Interface;

public interface ICreateLeilaoUseCase
{
    Task<Result<CreateLeilaoResponse>> Executar(CreateLeilaoRequest request);
}