using MediatR;

namespace VCORE.Application.Features.Teachers.Queries.GetById;

public class TeacherGetByIdQuery : IRequest<TeacherGetByIdResponseDto>
{
    public required Guid Id { get; set; }
}