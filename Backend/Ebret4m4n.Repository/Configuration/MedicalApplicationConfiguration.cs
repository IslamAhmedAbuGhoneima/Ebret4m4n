using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebret4m4n.Repository.Configuration;

public class MedicalApplicationConfiguration : IEntityTypeConfiguration<MedicalApplication>
{
    public void Configure(EntityTypeBuilder<MedicalApplication> builder)
    {
        builder.HasKey(j => j.ApplicationId);

        builder.Property(j => j.MedicalNumber)
            .HasMaxLength(25)
            .IsRequired();

        builder.Property(j => j.ApplicantPosition)
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(j => j.ApplicationStatus)
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();
    }
}
