using SistemaLeilao.Application.Response;

namespace SistemaLeilao.Application.Interface;

public interface IUserService
{
    Task CreateUser();
    Task<UserResponse>  GetUserByEmail(string email);
}