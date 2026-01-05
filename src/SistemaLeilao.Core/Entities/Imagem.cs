using SistemaLeilao.Core.Base;

namespace SistemaLeilao.Core.Entities;

public class Imagem : BaseEntity
{
    public Imagem(string url, Guid bemId)
    {
        Url = url;
        BemId = bemId;
    }

    public string Url { get; set; }
    public Guid BemId { get; set; }
}