using Core.Persistence.Entities;

namespace VCORE.Domain.Models;

public class Teacher : Entity<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime HireDate { get; set; }

    public ICollection<Course>? Courses { get; set; }
}