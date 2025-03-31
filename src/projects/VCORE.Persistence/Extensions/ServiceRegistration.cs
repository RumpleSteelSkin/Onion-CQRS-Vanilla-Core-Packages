using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VCORE.Application.Services.Repositories;
using VCORE.Persistence.Contexts;
using VCORE.Persistence.Repositories;

namespace VCORE.Persistence.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region Entity Framework Services

        //Microsoft.EntityFrameworkCore
        services.AddDbContext<BaseDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Home")));

        #endregion

        #region Repository Interface Define

        services.AddScoped<IClassroomRepository, ClassroomRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();

        #endregion
        
        return services;
    }
}