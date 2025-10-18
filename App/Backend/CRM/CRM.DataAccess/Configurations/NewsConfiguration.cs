using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class NewsConfiguration : IEntityTypeConfiguration<NewsEntity>
{
    public void Configure(EntityTypeBuilder<NewsEntity> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Title).IsRequired();
        builder.Property(n => n.Description).IsRequired();
        builder.Property(n => n.Date)
            .HasColumnType("date")
            .IsRequired();
        builder.Property(n => n.PhotoPath);
    }
}