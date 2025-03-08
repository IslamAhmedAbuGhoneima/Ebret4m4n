using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebret4m4n.Repository.Configuration;

public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        IdentityRole[] roles = [
            new () {Name = "parent",NormalizedName = "PARENT"},
            new () { Name = "doctor", NormalizedName = "DOCTOR" },
            new () { Name = "organizer", NormalizedName = "ORGANIZER" },
            new () { Name = "governorateAdmin", NormalizedName = "GOVERNORATEADMIN" },
            new () { Name = "cityAdmin", NormalizedName = "CITYADMIN" },
            new () { Name = "admin", NormalizedName = "ADMIN" }
        ];

        builder.HasData(roles);
    }
}
