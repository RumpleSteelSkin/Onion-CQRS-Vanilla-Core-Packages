using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using VCORE.Application.Services.Repositories;
using VCORE.Domain.Models;

namespace VCORE.Application.Features.Teachers.Commands.Update;

public class TeacherUpdateCommandHandler(ITeacherRepository teacherRepository, IMapper mapper)
    : IRequestHandler<TeacherUpdateCommand, string>
{
    public async Task<string> Handle(TeacherUpdateCommand request, CancellationToken cancellationToken)
    {
        await teacherRepository.UpdateAsync(mapper.Map(request, await teacherRepository.GetByIdAsync(id: request.Id,
                                                ignoreQueryFilters: true,
                                                include: false, enableTracking: false,
                                                cancellationToken: cancellationToken)??
                                                                throw new NotFoundException("Teacher not found")) ,
            cancellationToken: cancellationToken);
        return "Teacher is updated";
    }
}