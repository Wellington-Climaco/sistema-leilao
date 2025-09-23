
namespace SistemaLeilao.Application.Request.Lance
{
    public record CreateLanceRequest(string LeilaoId,decimal Valor,string UserId);
}
