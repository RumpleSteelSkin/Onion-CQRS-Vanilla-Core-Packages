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
        #region Default Services

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        #endregion

        #region JWT Token Services

        services.Configure<CustomTokenOptions>(configuration.GetSection("TokenOptions"));

        services.AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<BaseDbContext>()
            .AddDefaultTokenProviders();

        var tokenOptions = configuration.GetSection("TokenOptions").Get<CustomTokenOptions>();

        if (tokenOptions is null || string.IsNullOrEmpty(tokenOptions.SecurityKey))
            throw new InvalidOperationException("JWT SecurityKey cannot be null or empty!");

        if (tokenOptions.Audience is null || tokenOptions.Audience.Count == 0)
            throw new InvalidOperationException("JWT Audience cannot be null or empty!");

        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience[0],
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                };
            });

        #endregion

        #region Redis Services

        var redisConfig = configuration.GetSection("RedisConfigurations");
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = redisConfig[configuration.GetSection("ConnectionStringsSelector").Get<string>()!];
            opt.InstanceName = redisConfig["InstanceName"];
        });

        #endregion

        #region Global Exception Handling Services

        services.AddExceptionHandler<HttpExceptionHandler>();

        #endregion

        return services;
    }
}