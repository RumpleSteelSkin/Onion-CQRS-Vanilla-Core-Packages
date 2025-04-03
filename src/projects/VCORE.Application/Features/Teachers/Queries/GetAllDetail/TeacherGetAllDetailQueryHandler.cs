using AutoMapper;
using MediatR;
using VCORE.Application.Services.Repositories;

namespace VCORE.Application.Features.Teachers.Queries.GetAllDetail;

public class
    TeacherGetAllDetailQueryHandler(IMapper mapper, ITeacherRepository teacherRepository)
    : IRequestHandler<TeacherGetAllDetailQuery,
        ICollection<TeacherGetAllDetailResponseDto>>
{
    public async Task<ICollection<TeacherGetAllDetailResponseDto>> Handle(TeacherGetAllDetailQuery request,
        CancellationToken cancellationToken)
    {
        return mapper.Map<ICollection<TeacherGetAllDetailResponseDto>>(
            await teacherRepository.GetAllAsync(cancellationToken: cancellationToken));
    }
}