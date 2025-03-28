using Core.Persistence.Entities;

namespace VCORE.Domain.Models;

public class Course : Entity<Guid>
{
    public required string Name { get; set; }
    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public ICollection<StudentCourse>? StudentCourses { get; set; }
}