using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Request.Lance;
using SistemaLeilao.Application.Request.Leilao;
using SistemaLeilao.Application.Response;
using SistemaLeilao.Application.Response.Leilao;

namespace SistemaLeilao.API.Controllers;

[ApiController]
[Route("v1/")]
public class LeilaoController : ControllerBase
{
    private readonly ICreateLeilaoUseCase _createLeilaoUseCase;
    private readonly ISearchLeilaoUseCase _searchLeilaoUseCase;
    private readonly IInitializeLeilaoUseCase _initializeLeilaoUseCase;
    
    public LeilaoController(ICreateLeilaoUseCase createLeilaoUseCase, ISearchLeilaoUseCase searchLeilaoUseCase,IInitializeLeilaoUseCase initializeLeilaoUseCase)
    {
        _createLeilaoUseCase = createLeilaoUseCase;
        _searchLeilaoUseCase = searchLeilaoUseCase;
        _initializeLeilaoUseCase = initializeLeilaoUseCase;
    }
    
    [HttpPost]
    [Route("leilao/create")]
    public async Task<IActionResult> CreateLeilao([FromBody] CreateLeilaoRequest request)
    {
        var result = await _createLeilaoUseCase.Executar(request);

        if (result.IsFailed)
            return BadRequest(new DefaultResponse<CreateLeilaoResponse>(
                StatusCodes.Status400BadRequest,
                    result.Errors.Select(x=>x.Message).ToList())
            );
        
        var response = new DefaultResponse<CreateLeilaoResponse>(
            result.Value, 
            StatusCodes.Status201Created);
        
        return Created($"v1/leilao/{response.Data.Id}",response);
    }

    [HttpGet]
    [Route("leilao/{id}")]
    public async Task<IActionResult> FindById([FromRoute] string id)
    {
        try
        {
            bool converted = Guid.TryParse(id, out var convertedId);
            
            if (!converted)
                return BadRequest(new DefaultResponse<LeilaoResponse>(
                    StatusCodes.Status400BadRequest,
                    "Id inválido"));

            var result = await _searchLeilaoUseCase.Executar(convertedId);
        
            if(result.IsFailed)
                return NotFound(new DefaultResponse<LeilaoResponse>( StatusCodes.Status404NotFound,
                    result.Errors.Select(x => x.Message).ToList()));
        
            var response = new DefaultResponse<LeilaoResponse>(result.Value, StatusCodes.Status200OK);
            return Ok(response);
        }
        catch (Exception e)
        {
            // todo logging
            var response = new DefaultResponse<LeilaoResponse>(StatusCodes.Status500InternalServerError, "Internal server error");
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpPatch]
    [Route("leilao/{id}/inicializar")]
    public async Task<IActionResult> InitializeLeilao([FromRoute] string id)
    {
        bool isConverted = Guid.TryParse(id, out var convertedId);

        if (!isConverted)
            return BadRequest(new DefaultResponse<string>(StatusCodes.Status400BadRequest, "Id inválido"));

        var result = await _initializeLeilaoUseCase.Execute(convertedId);

        if (result.IsFailed)
            return BadRequest(new DefaultResponse<string>(StatusCodes.Status400BadRequest,result.Errors.Select(x=>x.Message)));

        return Ok(new DefaultResponse<string>("Leilão iniciado com sucesso",StatusCodes.Status200OK));
    }


}