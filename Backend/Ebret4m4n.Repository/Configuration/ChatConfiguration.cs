using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ebret4m4n.Repository.Configuration;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.Property(p => p.Message)
            .HasMaxLength(350);

        builder.HasOne(p => p.Sender)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.Receiver)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
