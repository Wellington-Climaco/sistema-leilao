using FluentResults;
using SistemaLeilao.Application.Request.Lance;

namespace SistemaLeilao.Application.Interface;

public interface ICreateLanceUseCase
{
    Task<Result> Execute(CreateLanceRequest request);
}
