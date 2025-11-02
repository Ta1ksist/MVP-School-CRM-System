using CRM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DataAccess.Configurations;

public class ChatParticipantEntityConfiguration : IEntityTypeConfiguration<ChatParticipantEntity>
{
    public void Configure(EntityTypeBuilder<ChatParticipantEntity> builder)
    {
        builder.HasKey(cp => new { cp.ChatRoomId, cp.UserId });
        builder.HasOne(cp => cp.ChatRoom)
            .WithMany(cr => cr.Participants)
            .HasForeignKey(cp => cp.ChatRoomId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(cp => cp.User)
            .WithMany()
            .HasForeignKey(cp => cp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}