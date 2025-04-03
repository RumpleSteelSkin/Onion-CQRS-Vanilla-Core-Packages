namespace VCORE.Application.Features.Teachers.Queries.GetAllDetail;

public class TeacherGetAllDetailResponseDto
{
    public Guid Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }

    //public ICollection<Course>? Courses { get; set; }
}