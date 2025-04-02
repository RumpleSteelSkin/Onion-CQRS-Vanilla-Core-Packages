using MediatR;

namespace VCORE.Application.Features.Teachers.Commands.CreateRange;

public class TeacherAddRangeCommand : IRequest<string>
{
    public required ICollection<TeacherAddRangeRequestDto> Entities { get; set; }
}