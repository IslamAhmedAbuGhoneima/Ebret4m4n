using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ebret4m4n.Repository.Configuration;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.Property(p=>p.Amount).IsRequired();


        builder.Property(p => p.HealthCareCenterId).IsRequired();

        builder.Property(p => p.HealthCareCenterName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.FirstDay)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.SecondDay)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.Governorate)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(p => p.City)
            .HasMaxLength(30);

        builder.Property(p => p.Village)
            .HasMaxLength(30);

        builder.HasIndex(p => p.HealthCareCenterId);
    }
}
