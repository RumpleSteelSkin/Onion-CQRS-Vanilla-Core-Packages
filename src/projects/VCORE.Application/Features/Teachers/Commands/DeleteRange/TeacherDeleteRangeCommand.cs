using MediatR;

namespace VCORE.Application.Features.Teachers.Commands.DeleteRange;

public class TeacherDeleteRangeCommand : IRequest<string>
{
    public ICollection<Guid>? TeacherIds { get; set; }
}