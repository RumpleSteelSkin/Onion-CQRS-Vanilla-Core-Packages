using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using VCORE.Application.Services.JwtServices;
using VCORE.Domain.Models;
using VCORE.Persistence.Contexts;
using VCORE.Presentation.Middlewares;

namespace VCORE.Presentation.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //JWT Token Config
        services.Configure<CustomTokenOptions>(configuration.GetSection("TokenOptions"));

        services.AddIdentity<User, IdentityRole<Guid>>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 8;
        }).AddEntityFrameworkStores<BaseDbContext>().AddDefaultTokenProviders();


        var tokenOption = configuration.GetSection("TokenOptions").Get<CustomTokenOptions>();
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
        {
            if (tokenOption != null)
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = tokenOption.Issuer,
                    ValidAudience = tokenOption.Audience?[0],
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption.SecurityKey ?? "Null"))
                };
        });
        
        //Global exception handling
        services.AddExceptionHandler<HttpExceptionHandler>();

        return services;
    }
}