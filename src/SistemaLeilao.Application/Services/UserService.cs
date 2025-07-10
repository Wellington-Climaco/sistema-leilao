using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Mapper;
using SistemaLeilao.Application.Response;
using SistemaLeilao.Core.Interface;
using SistemaLeilao.Core.ValueObject;

namespace SistemaLeilao.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task CreateUser()
    {
        throw new NotImplementedException();
    }

    public async Task<UserResponse> GetUserByEmail(string email)
    {
        var emailAddress = new Email(email);
        var entity = await _userRepository.GetUserByEmail(emailAddress);

        if (entity is null)
            throw new ArgumentNullException("usuário não encontrado.");

        var response = entity.MapToResponse();
        
        return response;
    }
}