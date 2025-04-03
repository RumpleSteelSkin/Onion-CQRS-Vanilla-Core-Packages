using MediatR;

namespace VCORE.Application.Features.Teachers.Queries.GetDetailById;

public class TeacherGetDetailByIdQuery : IRequest<TeacherGetDetailByIdResponseDto>
{
    public Guid Id { get; set; }
}