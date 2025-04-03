using System.Reflection;
using Bogus;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
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

        #region Bogus Services

        services.AddScoped(typeof(Faker<>));

        #endregion

        #region MediatR Services

        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            //Pipeline Registration
            opt.AddOpenBehavior(typeof(ValidationPipeline<,>));
            opt.AddOpenBehavior(typeof(AuthorizationPipeline<,>));
            opt.AddOpenBehavior(typeof(PerformancePipeline<,>));
            opt.AddOpenBehavior(typeof(LoggingPipeline<,>));
            opt.AddOpenBehavior(typeof(CacheRemovePipeline<,>));
            opt.AddOpenBehavior(typeof(AddCachePipeline<,>));
        });

        #endregion


        return services;
    }
}