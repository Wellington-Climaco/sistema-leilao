using FluentResults;

namespace SistemaLeilao.Application.Interface;

public interface IInitializeLeilaoUseCase
{
    Task<Result> Execute(Guid id);
}