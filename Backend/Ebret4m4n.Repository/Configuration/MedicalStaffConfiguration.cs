using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebret4m4n.Repository.Configuration;

public class MedicalStaffConfiguration : IEntityTypeConfiguration<MedicalStaff>
{
    public void Configure(EntityTypeBuilder<MedicalStaff> builder)
    {
        builder.Property(p => p.MedicalNumber)
            .HasMaxLength(25)
            .IsRequired();

        builder.Property(p => p.HealthCareCenterName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.HealthCareCenterGovernment)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(p => p.HealthCareCenterCity)
            .HasMaxLength(30);

        builder.Property(p => p.HealthCareCenterVillage)
            .HasMaxLength(30);

        builder.Property(p => p.FirstDay)
                    .HasConversion<string>()
                    .IsRequired();

        builder.Property(p => p.SecondDay)
            .HasConversion<string>()
            .IsRequired();
    }
}
