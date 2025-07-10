using SistemaLeilao.Core.Base;
using SistemaLeilao.Core.ValueObject;

namespace SistemaLeilao.Core;

public class Usuario : BaseEntity
{
    //orm
    private Usuario()
    {
        
    }
    
    public Usuario(string nome, Email email)
    {
        Nome = nome;
        Email = email;
    }
    
    public string Nome { get; set; }
    public Email Email { get; set; }
}