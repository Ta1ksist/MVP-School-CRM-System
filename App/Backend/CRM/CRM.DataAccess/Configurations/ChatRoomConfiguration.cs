using System.Text.Json;
using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoomEntity>
{
    public void Configure(EntityTypeBuilder<ChatRoomEntity> builder)
    {
        builder.HasKey(cr => cr.Id);
        builder.Property(cr => cr.Title)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(cr => cr.Type)
            .IsRequired();
        builder.HasMany(cr => cr.Messages)
            .WithOne(m => m.Room)
            .HasForeignKey(m => m.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(cr => cr.Participants)
            .WithOne(p => p.ChatRoom)
            .HasForeignKey(p => p.ChatRoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}