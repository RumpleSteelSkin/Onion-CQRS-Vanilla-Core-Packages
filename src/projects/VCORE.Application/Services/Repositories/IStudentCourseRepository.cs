using Core.Persistence.Repositories;
using VCORE.Domain.Models;

namespace VCORE.Application.Services.Repositories;

public interface IStudentCourseRepository:IAsyncRepository<StudentCourse,Guid>;