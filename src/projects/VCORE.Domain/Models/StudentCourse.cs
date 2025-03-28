using Core.Persistence.Entities;

namespace VCORE.Domain.Models;

public class StudentCourse : Entity<Guid>
{
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }

    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
}