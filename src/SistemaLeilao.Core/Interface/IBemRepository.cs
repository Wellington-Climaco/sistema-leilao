using SistemaLeilao.Core.Entities;

namespace SistemaLeilao.Core.Interface;

public interface IBemRepository
{
    Task Save(Bem entity);
}