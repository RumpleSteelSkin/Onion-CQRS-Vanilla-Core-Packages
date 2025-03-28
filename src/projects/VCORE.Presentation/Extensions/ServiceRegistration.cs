using Microsoft.AspNetCore.Identity;
using VCORE.Domain.Models;
using VCORE.Persistence.Contexts;

namespace VCORE.Presentation.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen();
        
        //Microsoft.AspNetCore.Identity
        services.AddIdentity<User, IdentityRole<Guid>>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 8;
        }).AddEntityFrameworkStores<BaseDbContext>().AddDefaultTokenProviders();
        
        
        
        return services;
    }
}