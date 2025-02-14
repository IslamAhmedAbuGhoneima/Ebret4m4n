using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ebret4m4n.Repository.Configuration;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        
        builder.Property(p=>p.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Message)
            .IsRequired()
            .HasMaxLength(350);
    }
}
