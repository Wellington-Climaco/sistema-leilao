using FluentResults;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Mapper.Lance;
using SistemaLeilao.Application.Request.Lance;
using SistemaLeilao.Core.Interface;
using SistemaLeilao.Infra.Repository;

namespace SistemaLeilao.Application.UseCase.Lance;

internal class CreateLanceUseCase : ICreateLanceUseCase
{
    private readonly ILanceRepository _lanceRepository;
    private readonly ILeilaoRepository _leilaoRepository;
    private readonly ISearchLeilaoUseCase _searchLeilaoUseCase;
    private readonly IUserService _userService;
    public CreateLanceUseCase(ILanceRepository lanceRepository, ISearchLeilaoUseCase searchLeilaoUseCase, IUserService userService, ILeilaoRepository leilaoRepository)
    {
        _lanceRepository = lanceRepository;
        _searchLeilaoUseCase = searchLeilaoUseCase;
        _userService = userService;
        _leilaoRepository = leilaoRepository;
    }

    public async Task<Result> Execute(CreateLanceRequest request)
    {
        try
        {
            bool isValidIdLeilao = Guid.TryParse(request.LeilaoId, out var leilaoId);
            bool isValidIdUser = Guid.TryParse(request.UserId, out var userId);

            if (!isValidIdLeilao || !isValidIdUser)
                return Result.Fail("Id é inválido");

            var leilao = await _leilaoRepository.FindById(leilaoId);
            var user = await _userService.GetUserById(userId);

            if (leilao is null)
                return Result.Fail("Leilão não encontrado");

            if (!user.IsSuccess)
                return Result.Fail(user.Errors.Select(x => x.Message));

            var melhorLance = await _lanceRepository.FindBetterLance(leilaoId);

            if(request.UserId.Equals(melhorLance.UsuarioId.ToString(),StringComparison.InvariantCultureIgnoreCase))
                return Result.Fail($"Você já possui o melhor lance, impossivel realizar outro");

            if (melhorLance != null && request.Valor < melhorLance.Valor + 10)
                return Result.Fail($"Já existe um lance melhor com o seguinte valor: R$ {melhorLance.Valor} - diferença minima entre os lances deve ser de 10 reais");           

            var lance = request.MapToEntity();

            leilao.AdicionarLance(lance);

            await _lanceRepository.Register(leilao.Lances.FirstOrDefault());

            return Result.Ok();
        }
        catch (Exception ex)
        {
            //TODO: add logging
            return Result.Fail(ex.Message);
        }
    }
}
