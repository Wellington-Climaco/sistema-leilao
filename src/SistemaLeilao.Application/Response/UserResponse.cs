using SistemaLeilao.Core;

namespace SistemaLeilao.Application.Response;

public record UserResponse(Guid Id,string Nome,string Email);