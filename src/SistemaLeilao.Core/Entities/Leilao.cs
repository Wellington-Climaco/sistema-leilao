using SistemaLeilao.Core.Base;
using SistemaLeilao.Core.Entities;
using SistemaLeilao.Core.Enum;

namespace SistemaLeilao.Core;

public class Leilao : BaseEntity
{
    public Bem Bem { get; set; }
    public Guid BemId { get; set; }
    
    public List<Lance> Lances { get; set; } = new();
    public DateTime Encerramento { get; set; }
    public decimal? ValorArrematado { get; private set; } = 0;
    public StatusLeilao Status { get; private set; }
    public DateTime? ArrematadoEm { get; private set; } = null;
    public TimeSpan IntervaloEntreLances { get; private set; } = TimeSpan.FromMinutes(3);

    //orm
    private Leilao()
    {
        
    }
    
    public Leilao(Bem bem, DateTime encerramento)
    {
        Bem = bem;
        Encerramento = encerramento;
        Status = StatusLeilao.Preparacao;
    }

    public void MudarStaus(StatusLeilao status)
    {
        if (Status == status)
            return;

        if(DateTime.Now > Encerramento)
            throw new InvalidOperationException("Impossível atualizar status de um leilão já finalizado.");

        Status = status;
    }
    public void Arrematar(decimal valorLance)
    {
        if(DateTime.Now < Encerramento)
            throw new InvalidOperationException("Horário do leilão ainda não chegou ao fim");
        
        if (Status == StatusLeilao.Finalizado)
            throw new InvalidOperationException("Leilão já finalizado");
        
        if (valorLance < Bem.ValorMinimo) 
            throw new ArgumentException("Lance abaixo do valor mínimo");
        
        ValorArrematado = valorLance;
        ArrematadoEm = DateTime.Now;
        Status = StatusLeilao.Finalizado;
    }

    public void AdicionarLance(Lance lance)
    {
        if (Status != StatusLeilao.Andamento) 
            throw new InvalidOperationException("Leilão não está em andamento");

        var horarioLimite = Encerramento.AddMinutes(-15);

        if (DateTime.Now > horarioLimite)
            throw new InvalidOperationException("horário limite já atingido.");
        
        if (lance.Valor < Bem.ValorMinimo) 
            throw new ArgumentException("Lance abaixo do valor mínimo");
        
        Lances.Add(lance);
    }

}