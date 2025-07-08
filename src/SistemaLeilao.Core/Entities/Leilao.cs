using SistemaLeilao.Core.Base;

namespace SistemaLeilao.Core;

public class Leilao : BaseEntity
{
    public Bem Bem { get; set; }
    public Guid BemId { get; set; }
    
    public List<Lance> Lances { get; set; } = new();
    public DateTime Encerramento { get; set; }   
    
    public decimal? ValorArrematado  { get; private set; }
    public bool Finalizado { get; private set; }
    public DateTime ArrematadoEm { get; private set; }

    //orm
    public Leilao()
    {
        
    }
    
    public Leilao(Bem bem, DateTime encerramento)
    {
        Bem = bem;
        Encerramento = encerramento;
    }

    public void Arrematar(decimal valorLance)
    {
        if(DateTime.Now < Encerramento)
            throw new InvalidOperationException("Horário do leilão ainda não chegou ao fim");
        
        if (Finalizado)
            throw new InvalidOperationException("Leilão já finalizado");
        
        if (valorLance < Bem.ValorMinimo) 
            throw new ArgumentException("Lance abaixo do valor mínimo");
        
        ValorArrematado = valorLance;
        ArrematadoEm = DateTime.Now;
        Finalizado = true;
    }

    public void AdicionarLance(Lance lance)
    {
        if (Finalizado) 
            throw new InvalidOperationException("Leilão finalizado");

        var horarioLimite = Encerramento.AddMinutes(-15);

        if (DateTime.Now > horarioLimite)
            throw new InvalidOperationException("horário limite já atingido.");
        
        Lances.Add(lance);
    }

}