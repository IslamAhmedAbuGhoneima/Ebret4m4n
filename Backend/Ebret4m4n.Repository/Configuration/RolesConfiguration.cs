using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebret4m4n.Repository.Configuration;

public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        IdentityRole[] roles = [
            new () { Name = "doctor", NormalizedName = "DOCTOR" },
            new () { Name = "nurse", NormalizedName = "NURSE" },
            new () { Name = "Admin", NormalizedName = "ADMIN" }
        ];

        builder.HasData(roles);
    }
}
