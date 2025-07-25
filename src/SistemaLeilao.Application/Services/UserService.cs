using FluentResults;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Mapper;
using SistemaLeilao.Application.Request;
using SistemaLeilao.Application.Response;
using SistemaLeilao.Core.Interface;
using SistemaLeilao.Core.ValueObject;

namespace SistemaLeilao.Application.Services;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResponse>> CreateUser(CreateUserRequest request)
    {
        var alreadyExist = await GetUserByEmail(request.email);
        
        if (alreadyExist.IsSuccess)
            return Result.Fail("Usuário com esse email já existe");
        
        var entity = request.MapToEntity();
        var result = await _userRepository.RegisterUser(entity);
        
        var response = result.MapToResponse();
        
        return Result.Ok(response);
    }

    public async Task<Result<UserResponse>> GetUserByEmail(string email)
    {
        var emailAddress = new Email(email);
        var entity = await _userRepository.GetUserByEmail(emailAddress);

        if (entity is null)
            return Result.Fail("usuário não encontrado.");

        var response = entity.MapToResponse();
        
        return Result.Ok(response);
    }

    public async Task<Result<UserResponse>> GetUserById(Guid id)
    {
        var result = await _userRepository.GetUserById(id);

        if (result is null)
            return Result.Fail("Usuário não encontrado");

        var response = result.MapToResponse();
        return Result.Ok(response);
    }
}