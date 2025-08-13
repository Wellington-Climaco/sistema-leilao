using FluentResults;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Mapper;
using SistemaLeilao.Application.Response.Leilao;
using SistemaLeilao.Core.Interface;

namespace SistemaLeilao.Application.UseCase;

public class SearchLeilaoUseCase : ISearchLeilaoUseCase
{
    private readonly ILeilaoRepository _leilaoRepository;

    public SearchLeilaoUseCase(ILeilaoRepository  leilaoRepository)
    {
        _leilaoRepository = leilaoRepository;
    }
    
    public async Task<Result<LeilaoResponse>> Executar(Guid id)
    {
        var entity = await _leilaoRepository.FindById(id);
        
        if (entity == null)
            return Result.Fail("Leilão não encontrado");

        var response = entity.MapLeilaoFindedByIdToResponse();
        
        return Result.Ok(response);
    }
}