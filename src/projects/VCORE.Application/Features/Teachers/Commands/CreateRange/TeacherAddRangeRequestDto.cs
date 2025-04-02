namespace VCORE.Application.Features.Teachers.Commands.CreateRange;

public class TeacherAddRangeRequestDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime HireDate { get; set; }
}