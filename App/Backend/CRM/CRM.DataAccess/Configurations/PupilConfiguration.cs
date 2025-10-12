using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class PupilConfiguration : IEntityTypeConfiguration<PupilEntity>
{
    public void Configure(EntityTypeBuilder<PupilEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.FirstName).IsRequired();
        builder.Property(p => p.LastName).IsRequired();
        builder.Property(p => p.DateOfBirth)
            .IsRequired()
            .HasColumnType("date");
        builder.Property(p => p.PhoneNumber).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Address).IsRequired();
        builder.HasOne(p => p.Grade)
            .WithMany(g => g.Pupils)
            .HasForeignKey(p => p.GradeId)
            .IsRequired();
        builder.HasMany(p => p.Parents)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}