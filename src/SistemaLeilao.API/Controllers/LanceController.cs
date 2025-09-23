using Microsoft.AspNetCore.Mvc;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Request.Lance;
using SistemaLeilao.Application.Response;

namespace SistemaLeilao.API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LanceController : ControllerBase
    {
        private readonly ICreateLanceUseCase _createLanceUseCase;
        public LanceController(ICreateLanceUseCase createLanceUseCase)
        {
            _createLanceUseCase = createLanceUseCase;
        }

        [HttpPost]
        [Route("/lance")]
        public async Task<IActionResult> CreateLance([FromBody] CreateLanceRequest request)
        {
            var isConvertedLeilao = Guid.TryParse(request.LeilaoId, out var leilaoId);
            var isConvertedUser = Guid.TryParse(request.UserId, out var userId);

            if (!isConvertedLeilao || !isConvertedUser)
                return BadRequest(new DefaultResponse<string>(StatusCodes.Status400BadRequest, "Id inválido"));

            var result = await _createLanceUseCase.Execute(request);

            if (!result.IsSuccess)
                return BadRequest(new DefaultResponse<string>(StatusCodes.Status400BadRequest, result.Errors.Select(x=>x.Message)));

            return Ok(new DefaultResponse<string>("Lance registrado com sucesso!",StatusCodes.Status200OK));
        }
    }
}
