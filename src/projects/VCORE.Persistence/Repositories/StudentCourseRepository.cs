using Core.Persistence.Repositories;
using VCORE.Application.Services.Repositories;
using VCORE.Domain.Models;
using VCORE.Persistence.Contexts;

namespace VCORE.Persistence.Repositories;

public class StudentCourseRepository(BaseDbContext context) : EntityFrameworkRepositoryBase<StudentCourse, Guid, BaseDbContext>(context), IStudentCourseRepository;