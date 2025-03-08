using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebret4m4n.Repository.Configuration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(25);


        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(e => e.UserName)
            .HasMaxLength(250)
            .IsRequired(false);


        builder.Property(e => e.Governorate)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(e => e.City)
            .HasMaxLength(150);

        builder.Property(e => e.Village)
            .HasMaxLength(150);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(e => e.HealthCareCenter)
            .WithMany(e => e.Users)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
