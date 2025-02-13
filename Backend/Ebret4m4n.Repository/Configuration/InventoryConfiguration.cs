using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Repository.Configuration
{
    internal class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.Property(p=>p.Amount).IsRequired();
            builder.HasIndex(p => p.HealthCareCenterId);

            builder.Property(p => p.HealthCareCenterId).IsRequired();
            builder.Property(p => p.HealthCareCenterName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.FirstDay).IsRequired();
            builder.Property(p => p.SecondDay).IsRequired();
            builder.Property(p => p.HealthCareLocation).IsRequired();
        }
    }
}
