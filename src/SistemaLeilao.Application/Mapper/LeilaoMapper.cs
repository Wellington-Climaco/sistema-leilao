using SistemaLeilao.Application.Mapper.Bem;
using SistemaLeilao.Application.Response.Bem;
using SistemaLeilao.Application.Response.Leilao;
using SistemaLeilao.Core;

namespace SistemaLeilao.Application.Mapper;

public static class MapLeilaoResponse
{
    public static CreateLeilaoResponse MapCreateLeilaoToResponse(this Core.Leilao  leilao)
    {
        var bemResponse = leilao.Bem.MapToResponse();
        return new CreateLeilaoResponse(leilao.Id,leilao.Status.ToString(),bemResponse);
    }

    public static LeilaoResponse MapLeilaoFindedByIdToResponse(this Leilao leilao)
    {
        var bemResponse = leilao.Bem.MapToResponse();
        return new LeilaoResponse(leilao.Id, leilao.Encerramento, leilao.ValorArrematado,
            leilao.Status.ToString(), leilao.VencedorId, leilao.IntervaloEntreLances,bemResponse);
    }

    public static AllLeilaoResponse MapLeilaoToResponseWithPagination(this IEnumerable<Leilao> leiloes,int total,int skip,
        int take,int currentPage,int totalPages)
    {    
        var toLeilaoResponse = leiloes.Select(leilao=> new LeilaoResponse(leilao.Id, leilao.Encerramento, leilao.ValorArrematado,
            leilao.Status.ToString(), leilao.VencedorId, leilao.IntervaloEntreLances, leilao.Bem.MapToResponse()));

        var response = new AllLeilaoResponse(total,currentPage,totalPages, skip, take,toLeilaoResponse);
        return response;
    }
}