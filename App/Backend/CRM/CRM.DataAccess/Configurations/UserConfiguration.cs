using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(u => u.PasswordHash)
            .IsRequired();
        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(20);
        builder.HasOne(u => u.Teacher)
            .WithOne(t => t.User)
            .HasForeignKey<UserEntity>(u => u.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(u => u.Directorate)
            .WithOne(d => d.User)
            .HasForeignKey<UserEntity>(u => u.DirectorateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}