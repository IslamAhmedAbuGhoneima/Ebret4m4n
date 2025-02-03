using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebret4m4n.Repository.Configuration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasDiscriminator<string>("user_type")
            .HasValue<Doctor>("doctor")
            .HasValue<AdminOfHC>("HC Admin")
            .HasValue<ApplicationUser>("parent");
    }
}
