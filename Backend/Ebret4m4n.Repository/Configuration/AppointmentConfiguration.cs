using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ebret4m4n.Repository.Configuration;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        
        builder.Property(p => p.Date)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasConversion<string>()
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(p => p.Location)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(p => p.UserId);

        builder.HasIndex(p => p.ChildId);
    }
}
