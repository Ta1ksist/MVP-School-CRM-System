using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class DirectorateConfiguration : IEntityTypeConfiguration<DirectorateEntity>
{
    public void Configure(EntityTypeBuilder<DirectorateEntity> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.FirstName).IsRequired();
        builder.Property(d => d.LastName).IsRequired();
        builder.Property(d => d.DateOfBirth).IsRequired().HasColumnType("date");
        builder.Property(d => d.PhotoPath).IsRequired();
        builder.Property(d => d.Role).IsRequired();
        builder.Property(d => d.PhoneNumber).IsRequired();
        builder.Property(d => d.Email).IsRequired();
        builder.Property(d => d.Address).IsRequired();
    }
}