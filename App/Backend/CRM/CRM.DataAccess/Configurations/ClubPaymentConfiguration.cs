using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class ClubPaymentConfiguration : IEntityTypeConfiguration<ClubPaymentEntity>
{
    public void Configure(EntityTypeBuilder<ClubPaymentEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Amount).IsRequired();
        builder.Property(c => c.PaymentMethod).IsRequired();
        builder.Property(c => c.PaymentDate)
            .HasColumnType("date")
            .IsRequired();
        builder.HasOne(p => p.Enrollment)
            .WithMany(e => e.Payments)
            .HasForeignKey(p => p.EnrollmentId);
    }
}