﻿using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ebret4m4n.Repository.Configuration;

public class HealthCareCenterConfiguration : IEntityTypeConfiguration<HealthCareCenter>
{
    public void Configure(EntityTypeBuilder<HealthCareCenter> builder)
    {
        builder.Property(p=>p.HealthCareCenterId).IsRequired();

        builder.Property(p => p.HealthCareCenterName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Governorate)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.City)
            .HasMaxLength(150);

        builder.Property(p => p.Village)
            .HasMaxLength(150);

        builder.Property(p => p.FirstDay)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.SecondDay)
            .HasConversion<string>()
            .IsRequired();

        builder.HasIndex(p => p.HealthCareCenterName);

    }
}
