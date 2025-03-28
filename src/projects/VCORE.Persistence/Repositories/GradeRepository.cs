using Core.Persistence.Repositories;
using VCORE.Application.Services.Repositories;
using VCORE.Domain.Models;
using VCORE.Persistence.Contexts;

namespace VCORE.Persistence.Repositories;

public class GradeRepository(BaseDbContext context) : EntityFrameworkRepositoryBase<Grade, Guid, BaseDbContext>(context), IGradeRepository;