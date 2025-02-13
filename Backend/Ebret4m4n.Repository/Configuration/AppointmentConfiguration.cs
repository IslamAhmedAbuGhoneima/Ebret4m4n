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
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            
            builder.Property(p => p.Date)
                .IsRequired();
            builder.Property(p => p.Status)
                .IsRequired();
            builder.Property(p => p.Location)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(p => p.UserId);
            builder.HasIndex(p => p.ChildId);
        }
    }
}
