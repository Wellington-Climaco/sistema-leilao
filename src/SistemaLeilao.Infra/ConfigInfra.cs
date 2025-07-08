using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaLeilao.Infra.ContextDb;

namespace SistemaLeilao.Infra;

public static class ConfigInfra
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connString));
        return services;
    }
}