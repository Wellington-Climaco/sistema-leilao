using SistemaLeilao.Core.Base;

namespace SistemaLeilao.Core;

public class Usuario : BaseEntity
{
    //orm
    public Usuario()
    {
        
    }
    
    public Usuario(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
    
    public string Nome { get; set; }
    public string Email { get; set; }
}