using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaLeilao.Core.Interface;
using SistemaLeilao.Infra.ContextDb;
using SistemaLeilao.Infra.Repository;

namespace SistemaLeilao.Infra;

public static class ConfigInfra
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigDbContext(services,configuration);
        ConfigDependencyInjection(services);
        return services;
    }
    private static void ConfigDbContext(IServiceCollection services,IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connString));
    }
    private static void ConfigDependencyInjection(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBemRepository, BemRepository>();
        services.AddScoped<ILeilaoRepository, LeilaoRepository>();
        
        
    }
}