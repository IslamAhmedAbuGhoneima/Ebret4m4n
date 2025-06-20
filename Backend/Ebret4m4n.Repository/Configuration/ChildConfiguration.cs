﻿using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ebret4m4n.Repository.Configuration;

public class ChildConfiguration : IEntityTypeConfiguration<Child>
{
    public void Configure(EntityTypeBuilder<Child> builder)
    {
        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasMaxLength(14);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.BirthDate)
            .IsRequired();

        builder.Property(p => p.AgeInMonth)
            .HasComputedColumnSql("CAST(DATEDIFF(DAY, BirthDate, GETDATE()) / 30.0 AS INT)", stored: false);

        builder.Property(p => p.Weight)
            .IsRequired();

        builder.Property(p => p.Gender)
            .IsRequired();

        builder.Property(p => p.PatientHistory)
            .HasMaxLength(2500);

        builder.HasIndex(p => p.Name);
    }
}
