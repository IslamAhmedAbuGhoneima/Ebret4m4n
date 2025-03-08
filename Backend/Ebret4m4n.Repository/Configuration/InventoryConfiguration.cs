using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ebret4m4n.Repository.Configuration;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.HasKey(I => new { I.HealthCareCenterId, I.Antigen });
            

        builder.Property(I=>I.Antigen)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p=>p.Amount)
            .IsRequired();

        builder.Property(p => p.HealthCareCenterId)
            .IsRequired();
    }
}
