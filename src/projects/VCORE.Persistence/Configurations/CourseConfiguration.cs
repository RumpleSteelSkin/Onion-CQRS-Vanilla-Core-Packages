using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VCORE.Domain.Models;

namespace VCORE.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");
        builder.HasQueryFilter(c => !c.IsDeleted);
        builder.Property(c => c.IsDeleted)
            .HasDefaultValue(false);

        // 1:N Relation: Course -> Teacher
        builder.HasOne(c => c.Teacher)
            .WithMany(t => t.Courses)
            .HasForeignKey(c => c.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);

        // N:M Relation: Course -> Students (StudentCourse)
        builder.HasMany(c => c.StudentCourses)
            .WithOne(sc => sc.Course)
            .HasForeignKey(sc => sc.CourseId);
    }
}