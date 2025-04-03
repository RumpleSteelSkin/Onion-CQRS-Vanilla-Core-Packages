using AutoMapper;
using MediatR;
using VCORE.Application.Services.Repositories;

namespace VCORE.Application.Features.Teachers.Queries.GetDetailById;

public class TeacherGetDetailByIdQueryHandler(IMapper mapper, ITeacherRepository teacherRepository)
    : IRequestHandler<TeacherGetDetailByIdQuery, TeacherGetDetailByIdResponseDto>
{
    public async Task<TeacherGetDetailByIdResponseDto> Handle(TeacherGetDetailByIdQuery request,
        CancellationToken cancellationToken)
    {
        return mapper.Map<TeacherGetDetailByIdResponseDto>(await teacherRepository.GetByIdAsync(request.Id,
            enableTracking: false, cancellationToken: cancellationToken));
    }
}