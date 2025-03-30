using System.Reflection;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Performance;
using Microsoft.Extensions.DependencyInjection;
using VCORE.Application.Services.JwtServices;

namespace VCORE.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IJwtService, JwtService>();
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //Pipeline Registration
            opt.AddOpenBehavior(typeof(PerformancePipeline<,>));
            opt.AddOpenBehavior(typeof(AuthorizationPipeline<,>));
        });

        return services;
    }
}