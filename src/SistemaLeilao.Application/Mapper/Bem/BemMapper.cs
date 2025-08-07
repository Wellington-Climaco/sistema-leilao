using SistemaLeilao.Application.Request.Bem;
using SistemaLeilao.Application.Response.Bem;
using SistemaLeilao.Core;
namespace SistemaLeilao.Application.Mapper.Bem;

public static class BemMapper 
{
    public static Core.Entities.Bem MapToEntity(this CreateBemRequest request) =>  new Core.Entities.Bem(request.Nome,request.ValorMinimo,request.Descricao);
    
    public static BemResponse MapToResponse(this Core.Entities.Bem entity) => new BemResponse(entity.Id,entity.Nome,entity.Descricao,entity.ValorMinimo);
}