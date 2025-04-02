using AutoMapper;
using MediatR;
using VCORE.Application.Services.Repositories;
using VCORE.Domain.Models;

namespace VCORE.Application.Features.Teachers.Commands.CreateRange;

public class TeacherAddRangeCommandHandler(ITeacherRepository teacherRepository, IMapper mapper)
    : IRequestHandler<TeacherAddRangeCommand, string>
{
    public async Task<string> Handle(TeacherAddRangeCommand request, CancellationToken cancellationToken)
    {
        await teacherRepository.AddRangeAsync(mapper.Map<ICollection<Teacher>>(request.Entities),
            cancellationToken: cancellationToken);
        return "Teachers Added Successfully";
    }
}