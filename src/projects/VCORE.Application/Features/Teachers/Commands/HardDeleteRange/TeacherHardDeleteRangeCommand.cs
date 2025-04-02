using MediatR;

namespace VCORE.Application.Features.Teachers.Commands.HardDeleteRange;

public class TeacherHardDeleteRangeCommand : IRequest<string>
{
    public ICollection<Guid>? TeacherIds { get; set; }
}