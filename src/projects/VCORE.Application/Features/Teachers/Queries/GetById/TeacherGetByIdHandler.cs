using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using VCORE.Application.Services.Repositories;

namespace VCORE.Application.Features.Teachers.Queries.GetById;

public class TeacherGetByIdHandler(ITeacherRepository teacherRepository, IMapper mapper)
    : IRequestHandler<TeacherGetByIdQuery, TeacherGetByIdResponseDto>
{
    public async Task<TeacherGetByIdResponseDto> Handle(TeacherGetByIdQuery request,
        CancellationToken cancellationToken)
    {
        return mapper.Map<TeacherGetByIdResponseDto>(
            await teacherRepository.GetByIdAsync(request.Id, ignoreQueryFilters: true, enableTracking: false,
                include: false,
                cancellationToken: cancellationToken)?? throw new NotFoundException("Teacher not found"));
    }
}