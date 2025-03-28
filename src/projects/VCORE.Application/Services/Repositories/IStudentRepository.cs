using Core.Persistence.Repositories;
using VCORE.Domain.Models;

namespace VCORE.Application.Services.Repositories;

public interface IStudentRepository:IAsyncRepository<Student,Guid>;