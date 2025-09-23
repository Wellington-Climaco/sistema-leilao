using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Services;
using SistemaLeilao.Application.UseCase;
using SistemaLeilao.Application.UseCase.Lance;
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
        services.AddScoped<IBemService, BemService>();
        services.AddScoped<ICreateLeilaoUseCase,CreateLeilaoUseCase>();
        services.AddScoped<ISearchLeilaoUseCase,SearchLeilaoUseCase>();
        services.AddScoped<IInitializeLeilaoUseCase,InitializeLeilaoUseCase>();
        services.AddScoped<ICreateLanceUseCase, CreateLanceUseCase>();
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
    }
}