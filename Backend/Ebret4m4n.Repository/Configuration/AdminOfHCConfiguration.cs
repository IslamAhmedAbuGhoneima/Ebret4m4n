using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebret4m4n.Repository.Configuration;

public class AdminOfHCConfiguration : IEntityTypeConfiguration<AdminOfHC>
{
    public void Configure(EntityTypeBuilder<AdminOfHC> builder)
    {
        builder.Property(p => p.NursingNumber)
            .IsRequired();

        builder.Property(p => p.HealthCareCenterName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.FirstDay)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.SecondDay)
            .HasConversion<string>()
            .IsRequired(); 

    }
}
