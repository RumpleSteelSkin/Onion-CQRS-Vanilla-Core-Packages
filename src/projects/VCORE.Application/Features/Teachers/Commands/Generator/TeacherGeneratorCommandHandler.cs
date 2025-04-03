using AutoMapper;
using Bogus;
using MediatR;
using VCORE.Application.Features.Teachers.Commands.CreateRange;
using VCORE.Domain.Models;

namespace VCORE.Application.Features.Teachers.Commands.Generator;

public class TeacherGeneratorCommandHandler(
    IMediator mediator,
    Faker<Teacher> faker,
    IMapper mapper) : IRequestHandler<TeacherGeneratorCommand, string>
{
    public async Task<string> Handle(TeacherGeneratorCommand request,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new TeacherAddRangeCommand()
        {
            Entities = mapper.Map<ICollection<TeacherAddRangeRequestDto>>(faker
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.HireDate, f => f.Date.Past())
                .Generate(request.GenerateCount))
        }, cancellationToken: cancellationToken);
    }
}