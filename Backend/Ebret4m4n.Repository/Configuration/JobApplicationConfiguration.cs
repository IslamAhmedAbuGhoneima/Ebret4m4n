using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebret4m4n.Repository.Configuration;

public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplications>
{
    public void Configure(EntityTypeBuilder<JobApplications> builder)
    {
        builder.HasKey(j => j.JobId);

        builder.Property(j => j.MedicalNumber)
            .HasMaxLength(25)
            .IsRequired();

        builder.Property(j => j.JobPosition)
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();
    }
}
