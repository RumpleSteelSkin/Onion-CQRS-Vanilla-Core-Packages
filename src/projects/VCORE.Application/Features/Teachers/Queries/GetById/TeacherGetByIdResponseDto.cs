namespace VCORE.Application.Features.Teachers.Queries.GetById;

public class TeacherGetByIdResponseDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
}