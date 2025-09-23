using SistemaLeilao.Application.Request.Lance;
using SistemaLeilao.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLeilao.Application.Mapper.Lance
{
    public static class LanceMapper
    {
        public static Core.Lance MapToEntity(this CreateLanceRequest request) 
            => new Core.Lance(request.Valor, Guid.Parse(request.LeilaoId), Guid.Parse(request.UserId));
    }
}
