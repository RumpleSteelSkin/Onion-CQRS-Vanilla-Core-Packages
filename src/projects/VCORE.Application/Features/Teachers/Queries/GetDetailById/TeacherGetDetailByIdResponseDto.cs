namespace VCORE.Application.Features.Teachers.Queries.GetDetailById;

public class TeacherGetDetailByIdResponseDto
{
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }

    //public ICollection<Course>? Courses { get; set; }
}