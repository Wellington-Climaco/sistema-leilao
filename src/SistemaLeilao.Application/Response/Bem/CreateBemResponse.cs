namespace SistemaLeilao.Application.Response.Bem;

public record CreateBemResponse(Guid Id,string nome,string descricao,decimal valorMinimo);