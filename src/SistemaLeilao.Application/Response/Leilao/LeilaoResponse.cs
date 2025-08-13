using SistemaLeilao.Application.Mapper.Bem;
using SistemaLeilao.Application.Response.Bem;
using SistemaLeilao.Core.Entities;

namespace SistemaLeilao.Application.Response.Leilao;

public record CreateLeilaoResponse(Guid Id,string Status,BemResponse Bem);

public record LeilaoResponse(Guid Id,DateTime Encerramento,decimal? ValorArrematado,string Status,DateTime? ArrematadoEm,TimeSpan IntervaloEntreLances,BemResponse Bem);
