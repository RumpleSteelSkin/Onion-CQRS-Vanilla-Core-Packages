using AutoMapper;
using MediatR;
using VCORE.Application.Services.Repositories;

namespace VCORE.Application.Features.Teachers.Queries.GetAll;

public class TeacherGetAllQueryHandler(ITeacherRepository teacherRepository, IMapper mapper)
    : IRequestHandler<TeacherGetAllQuery, ICollection<TeacherGetAllResponseDto>>
{
    public async Task<ICollection<TeacherGetAllResponseDto>> Handle(TeacherGetAllQuery request,
        CancellationToken cancellationToken)
    {
        return mapper.Map<ICollection<TeacherGetAllResponseDto>>(
            await teacherRepository.GetAllAsync(enableTracking: false, include: false,
                cancellationToken: cancellationToken));
    }
}