﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebret4m4n.Repository.Configuration;

public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        IdentityRole[] Roles = 
        {
            new ("Doctor"),
            new ("AdminOfHC"),
            new ("AdminOfMinistryOfHealth"),
        };

        builder.HasData(Roles);
    }
}
