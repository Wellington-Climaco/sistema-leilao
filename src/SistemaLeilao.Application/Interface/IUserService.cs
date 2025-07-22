using FluentResults;
using SistemaLeilao.Application.Request;
using SistemaLeilao.Application.Response;

namespace SistemaLeilao.Application.Interface;

public interface IUserService
{
    Task<Result<UserResponse>> CreateUser(CreateUserRequest request);
    Task<Result<UserResponse>>  GetUserByEmail(string email);
    Task<Result<UserResponse>>  GetUserById(Guid id);
}