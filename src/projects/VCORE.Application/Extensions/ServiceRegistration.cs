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
        #region AutoMapper Services
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        #endregion

        #region JWT Services
        services.AddScoped<IJwtService, JwtService>();
        #endregion

        #region MediatR Services

        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //Pipeline Registration
            opt.AddOpenBehavior(typeof(PerformancePipeline<,>));
            opt.AddOpenBehavior(typeof(AuthorizationPipeline<,>));
        });

        #endregion
        
        return services;
    }
}