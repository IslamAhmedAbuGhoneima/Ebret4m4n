using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebret4m4n.Repository.Configuration;

public class MedicalStaffConfiguration : IEntityTypeConfiguration<MedicalStaff>
{
    public void Configure(EntityTypeBuilder<MedicalStaff> builder)
    {
        builder.Property(p => p.HealthCareCenterName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.HealthCareCenterGovernorate)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.HealthCareCenterCity)
            .HasMaxLength(150);

        builder.Property(p => p.HealthCareCenterVillage)
            .HasMaxLength(150);

        builder.Property(p => p.FirstDay)
                    .HasConversion<string>()
                    .IsRequired();

        builder.Property(p => p.SecondDay)
            .HasConversion<string>()
            .IsRequired();
    }
}
