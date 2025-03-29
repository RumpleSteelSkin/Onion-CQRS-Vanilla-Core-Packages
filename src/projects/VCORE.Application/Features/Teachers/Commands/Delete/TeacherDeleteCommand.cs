using MediatR;

namespace VCORE.Application.Features.Teachers.Commands.Delete;

public class TeacherDeleteCommand : IRequest<string>
{
    public required Guid Id { get; set; }
}