using MediatR;

namespace VCORE.Application.Features.Teachers.Commands.HardDelete;

public class TeacherHardDeleteCommand : IRequest<string>
{
    public required Guid Id { get; set; }
}