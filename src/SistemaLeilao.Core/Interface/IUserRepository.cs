using SistemaLeilao.Core.ValueObject;

namespace SistemaLeilao.Core.Interface;

public interface IUserRepository
{
    Task<Usuario> RegisterUser(Usuario usuario);
    Task<Usuario?> GetUserByEmail(Email email);
    Task<Usuario?> GetUserById(Guid id);
}