using MediatR;

namespace VCORE.Application.Features.Teachers.Commands.Generator;

public class TeacherGeneratorCommand : IRequest<string>
{
    public int GenerateCount { get; set; } = 5;
}