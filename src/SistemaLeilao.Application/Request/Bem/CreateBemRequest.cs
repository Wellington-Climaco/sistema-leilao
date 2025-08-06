namespace SistemaLeilao.Application.Request.Bem;

public record CreateBemRequest(string Nome,string Descricao,decimal ValorMinimo);