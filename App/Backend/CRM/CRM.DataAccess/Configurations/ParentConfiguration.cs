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
        builder.Property(p => p.DateOfBirth)
            .HasColumnType("date")
            .IsRequired();
        builder.Property(p => p.Role).IsRequired();
        builder.Property(p => p.PhoneNumber).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Address).IsRequired();
        builder.HasMany(p => p.Pupils)
            .WithMany(pu => pu.Parents);

    }
}