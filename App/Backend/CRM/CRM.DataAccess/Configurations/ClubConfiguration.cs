using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class ClubConfiguration : IEntityTypeConfiguration<ClubEntity>
{
    public void Configure(EntityTypeBuilder<ClubEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Description).IsRequired();
        builder.Property(c => c.MonthlyFee).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(c => c.IsActive).IsRequired();
        builder.HasMany(c => c.Enrollments)
            .WithOne(e => e.Club)
            .HasForeignKey(e => e.ClubId)
            .IsRequired();
    }
}