using FluentResults;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Mapper.Bem;
using SistemaLeilao.Application.Request.Bem;
using SistemaLeilao.Application.Response.Bem;
using SistemaLeilao.Core.Interface;

namespace SistemaLeilao.Application.Services;

public class BemService : IBemService
{
    private readonly IBemRepository _bemRepository;

    public BemService(IBemRepository bemRepository)
    {
        _bemRepository = bemRepository;   
    }
    
    public async Task<Result<CreateBemResponse>> CreateBem(CreateBemRequest request)
    {
        var entity = request.MapToEntity();

        await _bemRepository.Save(entity);

        var response = entity.MapToResponse();
        
        return Result.Ok(response);
    }
}