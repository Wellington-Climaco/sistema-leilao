using FluentResults;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Request.Leilao;
using SistemaLeilao.Application.Response.Leilao;
using SistemaLeilao.Core;
using SistemaLeilao.Core.Interface;

namespace SistemaLeilao.Application.UseCase;

public class CreateLeilaoUseCase : ICreateLeilaoUseCase
{
    private readonly IBemRepository _bemRepository;
    private readonly ILeilaoRepository _leilaoRepository;

    public CreateLeilaoUseCase(IBemRepository bemRepository, ILeilaoRepository leilaoRepository)
    {
        _bemRepository = bemRepository;
        _leilaoRepository = leilaoRepository;
    }
    
    public async Task<Result<CreateLeilaoResponse>> Executar(CreateLeilaoRequest request)
    {
        Guid id;
        bool isConverted = Guid.TryParse(request.bemId,out id);
        
        if (!isConverted)
            return Result.Fail("Id do bem é inválido");
        
        var bem = await _bemRepository.FindById(id);

        if (bem is null)
            return Result.Fail("Bem não encontrado");
        
        var entity = new Leilao(bem, request.encerramento);

        await _leilaoRepository.RegisterLeilao(entity);

        var response = entity.MapToResponse();
        
        return Result.Ok(response);
    }
}