using SistemaLeilao.Application.Request;
using SistemaLeilao.Application.Response;
using SistemaLeilao.Core;
using SistemaLeilao.Core.ValueObject;

namespace SistemaLeilao.Application.Mapper;

public static class UsuarioMapper
{
    public static UserResponse MapToResponse(this Usuario usuario)
    {
        return new UserResponse(usuario.Id,usuario.Nome,usuario.Email.EmailAdress);
    }
    
    public static Usuario MapToEntity(this CreateUserRequest request) => new Usuario(request.nome, new Email(request.email));
}