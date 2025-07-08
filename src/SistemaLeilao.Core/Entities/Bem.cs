using SistemaLeilao.Core.Base;

namespace SistemaLeilao.Core;

public class Bem : BaseEntity
{
    //orm
    public Bem()
    {
        
    }
    public Bem(string nome, decimal valorMinimo)
    {
        Nome = nome;
        ValorMinimo = valorMinimo;
    }
    
    public string Nome { get; private set; }
    public decimal ValorMinimo { get; private set; }

    public void Arrematar(decimal valorLance)
    {
        if (valorLance < ValorMinimo)
            throw new ArgumentException();
    }
}