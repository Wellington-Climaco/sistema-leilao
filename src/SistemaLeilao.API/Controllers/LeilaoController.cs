using Microsoft.AspNetCore.Mvc;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Request.Leilao;
using SistemaLeilao.Application.Response;
using SistemaLeilao.Application.Response.Leilao;

namespace SistemaLeilao.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class LeilaoController : ControllerBase
{
    private readonly ICreateLeilaoUseCase _leilaoUseCase;
    
    public LeilaoController(ICreateLeilaoUseCase leilaoUseCase)
    {
        _leilaoUseCase = leilaoUseCase;
    }
    
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateLeilao([FromBody] CreateLeilaoRequest request)
    {
        var result = await _leilaoUseCase.Executar(request);

        if (result.IsFailed)
            return BadRequest(new DefaultResponse<CreateLeilaoResponse>(
                StatusCodes.Status400BadRequest.ToString(),
                    result.Errors.Select(x=>x.Message).ToList())
            );
        
        var response = new DefaultResponse<CreateLeilaoResponse>(
            result.Value, 
            StatusCodes.Status201Created.ToString());
        
        return Created($"v1/Leilao/create/{response.Data.Id}",response);
    }
}