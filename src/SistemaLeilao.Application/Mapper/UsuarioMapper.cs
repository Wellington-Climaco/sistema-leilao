using SistemaLeilao.Application.Response;
using SistemaLeilao.Core;

namespace SistemaLeilao.Application.Mapper;

public static class UsuarioMapper
{
    public static UserResponse MapToResponse(this Usuario usuario)
    {
        return new UserResponse(usuario.Nome,usuario.Email.EmailAdress);
    }
    
}