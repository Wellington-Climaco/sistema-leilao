using FluentResults;
using SistemaLeilao.Application.Response.Leilao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLeilao.Application.Interface
{
    public interface ISearchAllLeilaoUseCase
    {
        Task<Result<AllLeilaoResponse>> Executar(int skip,int take);
    }
}
