using MediatR;

namespace VCORE.Application.Features.Teachers.Commands.Update;

public class TeacherUpdateCommand : IRequest<string>
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime? HireDate { get; set; }
}