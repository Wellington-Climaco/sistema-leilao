using System.Runtime.InteropServices.JavaScript;

namespace SistemaLeilao.Core.Base;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}