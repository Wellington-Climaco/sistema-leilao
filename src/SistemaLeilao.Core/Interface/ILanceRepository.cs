using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLeilao.Core.Interface
{
    public interface ILanceRepository
    {
        Task<Lance?> GetById(Guid id);
        Task Register(Lance lance);
        Task<Lance> FindBetterLance(Guid leilaoId);
    }
}
