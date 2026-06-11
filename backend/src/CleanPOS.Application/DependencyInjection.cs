// src/CleanPOS.Application/DependencyInjection.cs
namespace CleanPOS.Application;

using CleanPOS.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // MediatR — enregistre tous les Handlers
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            // Pipeline — ordre d'exécution
            cfg.AddBehavior(typeof(IPipelineBehavior<,>),
                            typeof(LoggingBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>),
                            typeof(ValidationBehavior<,>));
        });

        // FluentValidation — enregistre tous les Validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}