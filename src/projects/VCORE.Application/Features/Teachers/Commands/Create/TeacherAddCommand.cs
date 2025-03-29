using MediatR;
namespace VCORE.Application.Features.Teachers.Commands.Create;
public class TeacherAddCommand : IRequest<string>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime HireDate { get; set; }
}