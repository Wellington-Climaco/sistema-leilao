using SistemaLeilao.Application.Mapper.Bem;
using SistemaLeilao.Application.Response.Bem;
using SistemaLeilao.Core.Entities;

namespace SistemaLeilao.Application.Response.Leilao;

public record CreateLeilaoResponse(Guid Id,string Status,BemResponse Bem);

public static class MapCreateLeilaoResponse
{
    public static CreateLeilaoResponse MapToResponse(this Core.Leilao  leilao)
    {
        var bemResponse = leilao.Bem.MapToResponse();
        return new CreateLeilaoResponse(Guid.NewGuid(),leilao.Status.ToString(),bemResponse);
    }
}