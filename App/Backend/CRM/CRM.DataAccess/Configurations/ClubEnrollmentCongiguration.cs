using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class ClubEnrollmentCongiguration : IEntityTypeConfiguration<ClubEnrollmentEntity>
{
    public void Configure(EntityTypeBuilder<ClubEnrollmentEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.EnrollmentDate)
            .HasColumnType("date")
            .IsRequired();
        builder.Property(c => c.IsActive).IsRequired();
        builder.HasOne(c => c.Club)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(c => c.ClubId);
        builder.Property(c => c.PupilId).IsRequired();
    }
}