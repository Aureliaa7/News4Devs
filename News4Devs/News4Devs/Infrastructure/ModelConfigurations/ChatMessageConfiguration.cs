using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News4Devs.Shared.Entities;
using System;

namespace News4Devs.Infrastructure.ModelConfigurations
{
    class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasOne(x => x.FromUser)
                .WithMany(x => x.MessagesFromUsers)
                .HasForeignKey(x => x.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ToUser)
                .WithMany(x => x.MessagesToUsers)
                .HasForeignKey(x => x.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
