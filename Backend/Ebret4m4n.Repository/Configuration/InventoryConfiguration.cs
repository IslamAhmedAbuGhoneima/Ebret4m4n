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
            .HasMaxLength(150);

        builder.Property(p => p.FirstDay)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.SecondDay)
            .HasConversion<string>()
            .IsRequired();

        
        builder.Property(p => p.HealthCareCenterGovernorate)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.HealthCareCenterCity)
            .HasMaxLength(150);

        builder.Property(p => p.HealthCareCenterVillage)
            .HasMaxLength(150);

        builder.HasMany(I => I.Vaccines)
            .WithOne();

        builder.HasIndex(p => p.HealthCareCenterId);
    }
}
