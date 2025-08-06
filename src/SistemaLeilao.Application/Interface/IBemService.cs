using FluentResults;
using SistemaLeilao.Application.Request.Bem;
using SistemaLeilao.Application.Response.Bem;

namespace SistemaLeilao.Application.Interface;

public interface IBemService
{
    Task<Result<CreateBemResponse>> CreateBem(CreateBemRequest request);
}