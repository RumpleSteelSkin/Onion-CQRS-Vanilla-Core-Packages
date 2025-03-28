using Core.Persistence.Entities;

namespace VCORE.Domain.Models;

public class Grade : Entity<Guid>
{
    public double Score { get; set; }
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
}