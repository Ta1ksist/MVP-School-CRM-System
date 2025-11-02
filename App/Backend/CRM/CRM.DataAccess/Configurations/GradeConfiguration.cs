using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<GradeEntity>
{
    public void Configure(EntityTypeBuilder<GradeEntity> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Name).IsRequired();
        builder.HasMany(x => x.Pupils)
            .WithOne(p => p.Grade)
            .HasForeignKey(p => p.GradeId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}