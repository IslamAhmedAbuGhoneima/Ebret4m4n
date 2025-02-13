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
    public class HealthCareCenterConfiguration : IEntityTypeConfiguration<HealthCareCenter>
    {
        public void Configure(EntityTypeBuilder<HealthCareCenter> builder)
        {
            builder.Property(p=>p.HealthCareCenterId).IsRequired();

            builder.Property(p => p.HealthCareCenterName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.HealthCareLocation)
                .IsRequired();

            builder.Property(p => p.FirstDay)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(p => p.SecondDay)
                .IsRequired()
                .HasMaxLength(15);

        }
    }
}
