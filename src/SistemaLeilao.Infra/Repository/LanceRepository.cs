using Microsoft.EntityFrameworkCore;
using SistemaLeilao.Core;
using SistemaLeilao.Core.Interface;
using SistemaLeilao.Infra.ContextDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLeilao.Infra.Repository
{
    internal class LanceRepository : ILanceRepository
    {
        private readonly AppDbContext _appDbContext;
        public LanceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Lance> FindBetterLance(Guid leilaoId)
        {
            var valor = await _appDbContext.Lances.AsNoTracking().Where(x=>x.LeilaoId == leilaoId).OrderByDescending(x=>x.Valor).FirstOrDefaultAsync();
            return valor;
        }

        public async Task<Lance?> GetById(Guid id)
        {
            return await _appDbContext.Lances.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Register(Lance lance)
        {
            await _appDbContext.Lances.AddAsync(lance);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
