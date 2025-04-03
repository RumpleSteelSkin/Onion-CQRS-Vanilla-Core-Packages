using MediatR;
using VCORE.Application.Services.Repositories;

namespace VCORE.Application.Features.Teachers.Queries.GetCount;

public class TeacherGetCountQueryHandler(ITeacherRepository teacherRepository) : IRequestHandler<TeacherGetCountQuery, int>
{
    public async Task<int> Handle(TeacherGetCountQuery request, CancellationToken cancellationToken)
    {
        return await teacherRepository.CountAsync(include:false,cancellationToken:cancellationToken);
    }
}