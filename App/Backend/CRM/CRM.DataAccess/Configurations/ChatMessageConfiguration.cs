using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessageEntity>
{
    public void Configure(EntityTypeBuilder<ChatMessageEntity> builder)
    {
        builder.HasKey(cm => cm.Id);
        builder.Property(cm => cm.Text)
            .IsRequired()
            .HasMaxLength(2000);
        builder.Property(cm => cm.SentAt)
            .IsRequired();
        builder.HasOne(cm => cm.Room)
            .WithMany(cr => cr.Messages)
            .HasForeignKey(cm => cm.RoomId);
    }
}