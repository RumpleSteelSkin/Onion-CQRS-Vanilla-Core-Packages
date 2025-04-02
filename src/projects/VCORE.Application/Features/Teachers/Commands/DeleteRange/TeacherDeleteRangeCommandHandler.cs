using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using VCORE.Application.Services.Repositories;

namespace VCORE.Application.Features.Teachers.Commands.DeleteRange;

public class TeacherDeleteRangeCommandHandler(ITeacherRepository teacherRepository)
    : IRequestHandler<TeacherDeleteRangeCommand, string>
{
    public async Task<string> Handle(TeacherDeleteRangeCommand request, CancellationToken cancellationToken)
    {
        await teacherRepository.DeleteRangeAsync((await teacherRepository.GetByIdsAsync(request.TeacherIds,
                enableTracking: false, include: false,
                cancellationToken: cancellationToken) ?? throw new NotFoundException("Teacher not found"))!,
            cancellationToken);
        return "Teachers Deleted";
    }
}