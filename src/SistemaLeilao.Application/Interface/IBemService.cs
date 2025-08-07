using FluentResults;
using SistemaLeilao.Application.Request.Bem;
using SistemaLeilao.Application.Response.Bem;

namespace SistemaLeilao.Application.Interface;

public interface IBemService
{
    Task<Result<BemResponse>> CreateBem(CreateBemRequest request);
    Task<Result<BemResponse>> GetById(Guid id);
}