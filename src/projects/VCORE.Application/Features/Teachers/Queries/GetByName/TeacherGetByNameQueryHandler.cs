using AutoMapper;
using MediatR;
using VCORE.Application.Services.Repositories;

namespace VCORE.Application.Features.Teachers.Queries.GetByName;

public class TeacherGetByNameQueryHandler(IMapper mapper, ITeacherRepository teacherRepository)
    : IRequestHandler<TeacherGetByNameQuery, ICollection<TeacherGetByNameResponseDto>>
{
    public async Task<ICollection<TeacherGetByNameResponseDto>> Handle(TeacherGetByNameQuery request,
        CancellationToken cancellationToken)
    {
        return mapper.Map<ICollection<TeacherGetByNameResponseDto>>(await teacherRepository.GetAllAsync(
            filter: x => (x.FirstName + " " + x.LastName).Contains(request.FullName),
            enableTracking: false, include: false, cancellationToken: cancellationToken));
    }
}