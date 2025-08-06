using SistemaLeilao.Core.Base;

namespace SistemaLeilao.Core.Entities;

public class Bem : BaseEntity
{
    //orm
    private Bem()
    {
        
    }
    public Bem(string nome, decimal valorMinimo,string descricao)
    {
        Nome = nome;
        ValorMinimo = valorMinimo;
        Descricao = descricao;
    }
    
    public string Nome { get; private set; }
    public string Descricao { get; set; }
    public decimal ValorMinimo { get; private set; }

    public void Arrematar(decimal valorLance)
    {
        if (valorLance < ValorMinimo)
            throw new ArgumentException();
    }
}