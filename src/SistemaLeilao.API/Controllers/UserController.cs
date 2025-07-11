using Microsoft.AspNetCore.Mvc;

namespace SistemaLeilao.API.Controllers;

[ApiController]
[Route("/user")]
public class UserController : ControllerBase
{
    
    [HttpPost]
    [Route("/user")]
    public async Task<IActionResult> CreateUser()
    {
        return Ok();
    }
}