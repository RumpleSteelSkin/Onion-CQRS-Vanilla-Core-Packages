using MediatR;

namespace VCORE.Application.Features.Teachers.Queries.GetAll;

public class TeacherGetAllQuery : IRequest<ICollection<TeacherGetAllResponseDto>>;