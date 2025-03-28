using Core.Persistence.Entities;

namespace VCORE.Domain.Models;

public class Student : Entity<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public ICollection<StudentCourse>? StudentCourses { get; set; }
    public ICollection<Grade>? Grades { get; set; }

    public Guid ClassroomId { get; set; }
    public Classroom? Classroom { get; set; }
}