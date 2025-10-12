using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class ParentConfiguration : IEntityTypeConfiguration<ParentEntity>
{
    public void Configure(EntityTypeBuilder<ParentEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.FirstName).IsRequired();
        builder.Property(p => p.LastName).IsRequired();
        builder.Property(p => p.DateOfBirth).IsRequired().HasColumnType("date");
        builder.Property(p => p.Role).IsRequired();
        builder.Property(p => p.PhoneNumber).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Address).IsRequired();
        builder.Property(p => p.PupilId).IsRequired();
        builder.HasOne(p => p.Pupil)
            .WithMany(pu => pu.Parents)
            .HasForeignKey(p => p.PupilId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}