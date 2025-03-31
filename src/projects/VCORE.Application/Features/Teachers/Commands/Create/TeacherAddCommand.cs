using Core.Application.Pipelines.Authorization;
using MediatR;
namespace VCORE.Application.Features.Teachers.Commands.Create;
public class TeacherAddCommand : IRequest<string>,IRoleExists
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime HireDate { get; set; }
    public string[] Roles { get; } = ["Admin"];
}