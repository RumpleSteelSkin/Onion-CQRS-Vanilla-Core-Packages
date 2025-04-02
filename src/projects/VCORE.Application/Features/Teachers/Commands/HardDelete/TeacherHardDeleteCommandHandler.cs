using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using VCORE.Application.Services.Repositories;

namespace VCORE.Application.Features.Teachers.Commands.HardDelete;

public class TeacherHardDeleteCommandHandler(ITeacherRepository teacherRepository)
    : IRequestHandler<TeacherHardDeleteCommand, string>
{
    public async Task<string> Handle(TeacherHardDeleteCommand request, CancellationToken cancellationToken)
    {
        await teacherRepository.HardDeleteAsync(
            await teacherRepository.GetByIdAsync(id: request.Id, ignoreQueryFilters: true, enableTracking: false,
                include: false,
                cancellationToken: cancellationToken) ?? throw new NotFoundException("Teacher not found!"),
            cancellationToken: cancellationToken);
        return "Teacher is purged";
    }
}