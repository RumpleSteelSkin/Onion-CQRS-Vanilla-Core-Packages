using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using VCORE.Application.Services.Repositories;

namespace VCORE.Application.Features.Teachers.Commands.HardDeleteRange;

public class TeacherHardDeleteRangeCommandHandler(ITeacherRepository teacherRepository)
    : IRequestHandler<TeacherHardDeleteRangeCommand, string>
{
    public async Task<string> Handle(TeacherHardDeleteRangeCommand request, CancellationToken cancellationToken)
    {
        await teacherRepository.HardDeleteRangeAsync((await teacherRepository.GetByIdsAsync(request.TeacherIds,
                ignoreQueryFilters: true,
                enableTracking: false, include: false,
                cancellationToken: cancellationToken) ?? throw new NotFoundException("Teacher not found"))!,
            cancellationToken);
        return "Teachers purged";
    }
}