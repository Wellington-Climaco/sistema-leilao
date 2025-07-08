using SistemaLeilao.Core.Base;

namespace SistemaLeilao.Core;

public class Lance : BaseEntity
{
    //orm
    public Lance()
    {
        
    }
    
    public decimal Valor { get; set; }
    
    public Guid BemId { get; set; }
    public Leilao Bem { get; set; }

    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}