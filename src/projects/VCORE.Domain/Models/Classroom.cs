using Core.Persistence.Entities;

namespace VCORE.Domain.Models;

public class Classroom : Entity<Guid>
{
    public required string Name { get; set; }
    public ICollection<Student>? Students { get; set; }
}