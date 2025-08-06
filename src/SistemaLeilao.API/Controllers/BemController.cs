using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Request.Bem;
using SistemaLeilao.Application.Response;
using SistemaLeilao.Application.Response.Bem;

namespace SistemaLeilao.API.Controllers;

[ApiController]
[Route("/v1")]
public class BemController : ControllerBase
{
    private readonly IBemService _bemService;
    private readonly IValidator<CreateBemRequest> _validator;

    public BemController(IBemService bemService, IValidator<CreateBemRequest> validator)
    {
        _bemService = bemService;
        _validator = validator;
    }
    
    [HttpPost]
    [Route("bem/create")]
    public async Task<IActionResult> CreateBem(CreateBemRequest request)
    {
        var validation = await _validator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(x => x.ErrorMessage).ToList();
            var defaultResponse = new DefaultResponse<string>(StatusCodes.Status400BadRequest.ToString(), errors);
            return BadRequest(defaultResponse);
        }
        
        var result = await _bemService.CreateBem(request);

        if (result.IsFailed)
            return BadRequest(new DefaultResponse<string>( StatusCodes.Status400BadRequest.ToString(),
                result.Errors.Select(x=>x.Message).ToList()));
        
        var response = new DefaultResponse<CreateBemResponse>(result.Value, StatusCodes.Status201Created.ToString());
        
        return Created($"v1/bem/create/{result.Value.Id}", response);
    }
}