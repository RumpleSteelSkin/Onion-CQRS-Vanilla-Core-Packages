using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VCORE.Domain.Models;

namespace VCORE.Persistence.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.ToTable("Grades");
        builder.HasQueryFilter(g => !g.IsDeleted);
        builder.Property(g => g.IsDeleted)
            .HasDefaultValue(false);
        builder.Property(g => g.Name)
            .IsRequired();

        // 1:N Relation: Grade -> Student
        builder.HasOne(g => g.Student)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1:N Relation: Grade -> Course
        builder.HasOne(g => g.Course)
            .WithMany(c => c.Grades)
            .HasForeignKey(g => g.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}