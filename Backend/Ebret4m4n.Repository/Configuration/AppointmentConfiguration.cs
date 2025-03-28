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

        builder.Property(p => p.Location)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(p => p.VaccineName)
            .HasMaxLength(150);

        builder.HasIndex(p => p.UserId);

        builder.HasIndex(p => p.ChildId);
    }
}
