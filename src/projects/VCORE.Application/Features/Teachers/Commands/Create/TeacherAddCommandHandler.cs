using AutoMapper;
using MediatR;
using VCORE.Application.Services.Repositories;
using VCORE.Domain.Models;
namespace VCORE.Application.Features.Teachers.Commands.Create;
public class TeacherAddCommandHandler(ITeacherRepository teacherRepository, IMapper mapper)
    : IRequestHandler<TeacherAddCommand, string>
{
    public async Task<string> Handle(TeacherAddCommand request, CancellationToken cancellationToken)
    {
        await teacherRepository.AddAsync(mapper.Map<Teacher>(request), cancellationToken);
        return "Teacher is added.";
    }
}