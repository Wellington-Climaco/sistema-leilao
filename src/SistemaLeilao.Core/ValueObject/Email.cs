using System.Text.RegularExpressions;

namespace SistemaLeilao.Core.ValueObject;

public class Email
{
    private static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);
    
    //orm
    private Email()
    {
        
    }
    
    public Email(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-mail não pode ser vazio");
        
        if(!EmailRegex.IsMatch(email))
            throw new ArgumentException("Formato de e-mail inválido.");
        
        EmailAdress = email;
    }
    public string EmailAdress { get; private set; }
}