using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoomEntity>
{
    public void Configure(EntityTypeBuilder<ChatRoomEntity> builder)
    {
        builder.HasKey(cr => cr.Id);
        builder.Property(cr => cr.Title).IsRequired();
        builder.Property(cr => cr.Type).IsRequired();
        builder.Property(cr => cr.Participants).HasColumnType("text");
    }
}