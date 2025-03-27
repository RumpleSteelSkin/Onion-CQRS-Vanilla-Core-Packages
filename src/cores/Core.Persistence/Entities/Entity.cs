namespace Core.Persistence.Entities;

public abstract class Entity<TId>
{
    public TId Id { get; set; } = default!;
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public DateTime? UpdatedTime { get; set; }
    public bool IsDeleted { get; set; } = false;
}