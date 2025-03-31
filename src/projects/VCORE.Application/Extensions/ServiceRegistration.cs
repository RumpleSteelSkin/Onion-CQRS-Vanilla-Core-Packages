using System.Reflection;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Performance;
using Core.Application.Pipelines.Validation;
using Core.CrossCuttingConcerns.Loggers.Serilog.Loggers;
using Core.CrossCuttingConcerns.Loggers.Serilog.ServiceBase;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VCORE.Application.Services.JwtServices;

namespace VCORE.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        #region AutoMapper Services

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        #endregion

        #region JWT Services

        services.AddScoped<IJwtService, JwtService>();

        #endregion

        #region Serilog Services

        services.AddTransient<LoggerService, MsSqlLogger>();
        // services.AddTransient<LoggerService, FileLogger>();

        #endregion

        #region Fluent Validation Services

        services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);

        #endregion

        #region MediatR Services

        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            
            //Pipeline Registration
            opt.AddOpenBehavior(typeof(PerformancePipeline<,>));
            opt.AddOpenBehavior(typeof(AuthorizationPipeline<,>));
            opt.AddOpenBehavior(typeof(LoggingPipeline<,>));
            opt.AddOpenBehavior(typeof(ValidationPipeline<,>));
        });

        #endregion

        return services;
    }
}