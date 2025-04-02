using MediatR;

namespace VCORE.Application.Features.Teachers.Commands.UpdateRange;

public class TeacherUpdateRangeCommand : IRequest<string>
{
    public required ICollection<TeacherUpdateRangeRequestDto> Entities { get; set; }
}