using SistemaLeilao.Core.Base;

namespace SistemaLeilao.Core;

public class Lance : BaseEntity
{
    //orm
    private Lance()
    {
        
    }
    
    public decimal Valor { get; set; }
    
    public Guid LeilaoId { get; set; }
    public Leilao Leilao { get; set; }

    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}