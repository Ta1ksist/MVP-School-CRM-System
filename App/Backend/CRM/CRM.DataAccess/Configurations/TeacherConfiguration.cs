using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<TeacherEntity>
{
    public void Configure(EntityTypeBuilder<TeacherEntity> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.FirstName).IsRequired();
        builder.Property(t => t.LastName).IsRequired();
        builder.Property(t => t.DateOfBirth)
            .HasColumnType("date")
            .IsRequired();
        builder.Property(t => t.PhotoPath).IsRequired();
        builder.Property(t => t.PhoneNumber).IsRequired();
        builder.Property(t => t.Email).IsRequired();
        builder.Property(t => t.Address).IsRequired();
        builder.HasMany(t => t.Subjects)
            .WithMany(s => s.Teachers)
            .UsingEntity(j => j.ToTable("TeacherSubjects"));
    }
}