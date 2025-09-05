using FluentResults;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Core.Enum;
using SistemaLeilao.Core.Interface;

namespace SistemaLeilao.Application.UseCase;

public class InitializeLeilaoUseCase : IInitializeLeilaoUseCase
{
    private readonly ILeilaoRepository _leilaoRepository;
    public InitializeLeilaoUseCase(ILeilaoRepository leilaoRepository)
    {
        _leilaoRepository = leilaoRepository;
    }

    public async Task<Result> Execute(Guid id)
    {
        try
        {
            var leilao = await _leilaoRepository.FindById(id);

            if (leilao is null)
                return Result.Fail("Leilão não encontrado");

            leilao.MudarStaus(StatusLeilao.Andamento);

            await _leilaoRepository.InitializeLeilao(leilao);

            return Result.Ok();
        }
        catch(InvalidOperationException exception)
        {
            return Result.Fail(exception.Message);
        }   
    }
}