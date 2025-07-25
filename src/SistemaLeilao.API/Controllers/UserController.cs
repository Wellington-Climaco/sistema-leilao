using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Request;

namespace SistemaLeilao.API.Controllers;

[ApiController]
[Route("/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<CreateUserRequest> _validator;
    
    public UserController(IUserService userService, IValidator<CreateUserRequest> validator)
    {
        _userService = userService;
        _validator = validator;
    }
    
    [HttpPost]
    [Route("/create")]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        var validation = await _validator.ValidateAsync(request);
        
        if (!validation.IsValid)
        {
            var erorResponse = new HttpValidationProblemDetails(validation.ToDictionary())
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Error",
                Detail = "Falha ao criar usuário"
            };

            return BadRequest(erorResponse);
        }

        var result = await _userService.CreateUser(request);
        
        return Created($"/user/{result.Value.Id}",result.Value);
    }
    
    [HttpGet]
    [Route("/user/{email}")]
    public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
    {
            var result = await _userService.GetUserByEmail(email);

            if (result.IsFailed)
                return BadRequest(result.Errors);
        
            return Ok(result.Value);
    }
    
    [HttpGet]
    [Route("/user/{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] string id)
    {
        var idConverted = Guid.Parse(id);
        var result = await _userService.GetUserById(idConverted);

        if (result.IsFailed)
            return BadRequest(result.Errors);
        
        return Ok(result.Value);
    }
}