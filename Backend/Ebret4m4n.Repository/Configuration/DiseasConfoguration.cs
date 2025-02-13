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
    internal class DiseasConfoguration : IEntityTypeConfiguration<Diseas>
    {
        public void Configure(EntityTypeBuilder<Diseas> builder)
        {
            
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(x => x.Severity)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(p => p.Name);
            builder.HasIndex(p => p.ChildId);

        }
    }
}
