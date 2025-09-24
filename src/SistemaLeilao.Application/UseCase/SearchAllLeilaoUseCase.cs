using FluentResults;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Mapper;
using SistemaLeilao.Application.Response.Leilao;
using SistemaLeilao.Core.Interface;
using SistemaLeilao.Infra.ContextDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLeilao.Application.UseCase
{
    internal class SearchAllLeilaoUseCase : ISearchAllLeilaoUseCase
    {
        private readonly ILeilaoRepository _leilaoRepository;
        public SearchAllLeilaoUseCase(ILeilaoRepository leilaoRepository)
        {
            _leilaoRepository = leilaoRepository;
        }

        public async Task<Result<AllLeilaoResponse>> Executar(int skip, int take)
        {
            if (take > 1000)
                return Result.Fail("Não é possível retornar mais que 1000 registros em uma única consulta");

            var result = await _leilaoRepository.FindByPagination(skip, take);

            int totalPages = (int)Math.Ceiling(result.total / (double)take);

            int currentPage = (skip / take) + 1;

            return result.leiloes.MapLeilaoToResponseWithPagination(result.total,skip,take,currentPage,totalPages);           
        }
    }
}
