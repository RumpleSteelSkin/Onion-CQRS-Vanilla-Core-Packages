using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VCORE.Domain.Models;

namespace VCORE.Persistence.Configurations;

public class ClassroomConfiguration : IEntityTypeConfiguration<Classroom>
{
    public void Configure(EntityTypeBuilder<Classroom> builder)
    {
        builder.ToTable("Classrooms");
        builder.HasQueryFilter(c => !c.IsDeleted);
        builder.Property(c => c.IsDeleted)
            .HasDefaultValue(false);

        // 1:N Relation: Classroom -> Students
        builder.HasMany(c => c.Students)
            .WithOne(s => s.Classroom)
            .HasForeignKey(s => s.ClassroomId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}