namespace VCORE.Application.Features.Teachers.Queries.GetAll;

public class TeacherGetAllResponseDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
}