using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using VCORE.Application.Services.Repositories;

namespace VCORE.Application.Features.Teachers.Commands.Delete;

public class TeacherDeleteCommandHandler(ITeacherRepository teacherRepository)
    : IRequestHandler<TeacherDeleteCommand, string>
{
    public async Task<string> Handle(TeacherDeleteCommand request, CancellationToken cancellationToken)
    {
        await teacherRepository.DeleteAsync(
            await teacherRepository.GetByIdAsync(id: request.Id, enableTracking: false, include: false,
                cancellationToken: cancellationToken) ?? throw new NotFoundException("Teacher not found!"),
            cancellationToken: cancellationToken);
        return "Teacher is deleted";
    }
}