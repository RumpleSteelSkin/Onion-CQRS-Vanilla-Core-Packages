using MediatR;

namespace VCORE.Application.Features.Teachers.Queries.GetByName;

public class TeacherGetByNameQuery : IRequest<ICollection<TeacherGetByNameResponseDto>>
{
    public string FullName { get; set; } = string.Empty;
}