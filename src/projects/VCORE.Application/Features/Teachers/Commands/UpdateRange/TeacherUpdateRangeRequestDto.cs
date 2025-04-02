namespace VCORE.Application.Features.Teachers.Commands.UpdateRange;

public class TeacherUpdateRangeRequestDto
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime HireDate { get; set; }
}