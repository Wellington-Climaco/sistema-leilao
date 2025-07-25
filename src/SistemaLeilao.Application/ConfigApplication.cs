using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Services;
using SistemaLeilao.Application.Validators;

namespace SistemaLeilao.Application;

public static class ConfigApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        ConfigDependencyInjection(services);
        return services;
    }

    private static void ConfigDependencyInjection(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
    }
}