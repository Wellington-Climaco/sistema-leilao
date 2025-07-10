using Microsoft.EntityFrameworkCore;
using SistemaLeilao.Core;
using SistemaLeilao.Core.Interface;
using SistemaLeilao.Core.ValueObject;
using SistemaLeilao.Infra.ContextDb;

namespace SistemaLeilao.Infra.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Usuario> RegisterUser(Usuario usuario)
    {
        await _dbContext.AddAsync(usuario);
        await _dbContext.SaveChangesAsync();
        
        return usuario;
    }

    public async Task<Usuario> GetUserByEmail(Email email)
    {
       var result = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Email.EmailAdress == email.EmailAdress);
       return result;
    }
}