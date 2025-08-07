namespace SistemaLeilao.Application.Response.Bem;

public record BemResponse(Guid Id,string nome,string descricao,decimal valorMinimo);