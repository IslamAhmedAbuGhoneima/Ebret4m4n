using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ebret4m4n.Repository.Configuration;

public class VaccineConfiguration : IEntityTypeConfiguration<Vaccine>
{
    public void Configure(EntityTypeBuilder<Vaccine> builder)
    {
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.ChildAge)
            .IsRequired();

        builder.Property(x => x.IsTaken)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.ChildId);

    }
}
