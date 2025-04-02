using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using VCORE.Application.Services.Repositories;
using VCORE.Domain.Models;

namespace VCORE.Application.Features.Teachers.Commands.UpdateRange;

public class TeacherUpdateRangeCommandHandler(ITeacherRepository teacherRepository, IMapper mapper)
    : IRequestHandler<TeacherUpdateRangeCommand, string>
{
    public async Task<string> Handle(TeacherUpdateRangeCommand request, CancellationToken cancellationToken)
    {
        var teachers = mapper.Map<ICollection<Teacher>>(request.Entities);
        var existingTeachers = await teacherRepository.GetByIdsAsync(teachers.Select(t => t.Id).ToList(),
            enableTracking: false, include: false,
            cancellationToken: cancellationToken);
        foreach (var teacher in teachers)
        {
            var existingTeacher = existingTeachers.FirstOrDefault(t => t!.Id == teacher.Id);
            if (existingTeacher == null)
                throw new NotFoundException($"Teacher with ID {teacher.Id} not found.");
            mapper.Map(teacher, existingTeacher);
        }

        await teacherRepository.UpdateRangeAsync(existingTeachers!, cancellationToken);
        return "Teachers Updated";
    }
}