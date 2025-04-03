namespace VCORE.Application.Features.Teachers.Queries.GetByName;

public class TeacherGetByNameResponseDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
}